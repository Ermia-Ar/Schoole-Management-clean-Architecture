using AutoMapper;
using Core.Application.DTOs.Authentication;
using Core.Application.Interfaces;
using Core.Domain.Helper;
using Infrastructure.Identity.CurrentUserServices;
using Infrastructure.Identity.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private ICurrentUserServices _currentUserServices { get; set; }
        private AppIdentityDbContext _dbContext { get; set; }
        private IConfiguration _configuration { get; set; }
        private IMapper _mapper { get; set; }



        public AuthService(UserManager<ApplicationUser> userManager,
            IConfiguration configuration, IMapper mapper, AppIdentityDbContext dbContext, ICurrentUserServices currentUserServices)
        {
            //_signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _dbContext = dbContext;
            _currentUserServices = currentUserServices;
        }

        public async Task<bool> SignInAsync(LoginInRequest signInRequest)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.CodeMelly == signInRequest.CodeMelly);
            //check email or user name
            if (user == null)
            {
                return false;
            }
            // check password 
            bool checkPassword = await _userManager.CheckPasswordAsync(user, signInRequest.Password);
            if (!checkPassword)
            {
                return false;
            }

            return true;

        }

        public async Task<ApplicationUser?> FindByEmailOrUsername(string emailOrUSerName)
        {
            var Email = await _userManager.FindByEmailAsync(emailOrUSerName);
            if (Email != null)
            {
                return Email;
            }

            var Name = await _userManager.FindByNameAsync(emailOrUSerName);
            if (Name != null)
            {
                return Name;
            }

            return null;
        }

        public async Task<JwtAuthResult> GetJWTToken(string codeMelly)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.CodeMelly == codeMelly);

            var (JwtToken, AccessToken) = await GenerateJwtToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(10),
                IsUsed = false,
                IsRevoked = false,
                JwtId = JwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = AccessToken,
                UserId = user.Id
            };
            await _dbContext.userRefreshToken.AddAsync(userRefreshToken);
            await _dbContext.SaveChangesAsync();
            var response = new JwtAuthResult()
            {
                AccessToken = AccessToken,
                refreshToken = refreshToken,
            };
            return response;
        }

        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = await GetClaims(user);
            var JwtToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            var AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            return (JwtToken, AccessToken);
        }

        private RefreshToken GetRefreshToken(string UserName)
        {
            var refreshToken = new RefreshToken()
            {
                TokenString = GenerateRefreshToken(),
                UserName = UserName,
                ExpireAt = DateTime.Now.AddDays(10),
            };
            return refreshToken;
        }

        private string GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];
            var RandomNumberGenerate = RandomNumberGenerator.Create();
            RandomNumberGenerate.GetBytes(RandomNumber);
            return Convert.ToBase64String(RandomNumber);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var Roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimsModel.UserName),user.UserName),
                new Claim(nameof(UserClaimsModel.CodeMelly),user.CodeMelly),
                new Claim(nameof(UserClaimsModel.PhoneNumber),user.PhoneNumber),
                new Claim(nameof(UserClaimsModel.Id),user.Id.ToString()),
            };
            // adding roles
            foreach (var role in Roles)
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            return claims;
        }

        public async Task<JwtAuthResult> GetRefreshToken(string codeMelly, JwtSecurityToken JwtToken
            , DateTime? ExpiryDate, string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.CodeMelly == codeMelly);

            if (user == null)
            {
                throw new SecurityTokenException("User Is Not Found");
            }
            var (jwtSecurityToken, NewToken) = await GenerateJwtToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = NewToken;

            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)ExpiryDate;
            //get user name
            var username = JwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.UserName)).Value;
            refreshTokenResult.UserName = username;
            response.refreshToken = refreshTokenResult;
            return response;
        }

        public JwtSecurityToken ReadJwtToken(string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                throw new ArgumentNullException(nameof(AccessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(AccessToken);
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateAudience = true,
                ValidateLifetime = true,
            };

            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id)).Value;
            var userRefreshToken = await _dbContext.userRefreshToken.FirstOrDefaultAsync(x => x.Token == AccessToken
                                             && x.RefreshToken == RefreshToken &&
                                             x.UserId == userId.ToString());

            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow ||
                userRefreshToken.IsUsed == true || userRefreshToken.IsRevoked == true)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = true;
                _dbContext.userRefreshToken.Update(userRefreshToken);
                return ("Refresh Token Is Expired Please Login Again", null);
            }
            await _dbContext.SaveChangesAsync();
            //get code melly 
            var codeMelly = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.CodeMelly)).Value;
            var ExpireDate = userRefreshToken.ExpiryDate;
            return (codeMelly, ExpireDate);
        }

        public async Task ExpiredRefreshToken(string refreshToken)
        {
            var token = await _dbContext.userRefreshToken.
                SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);

            token.IsRevoked = true;
            token.IsUsed = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ForgotPasswordResponse?> ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            //check code melly and phone number 
            var user = await _userManager.Users.
                FirstOrDefaultAsync(x => x.CodeMelly == forgotPassword.CodeMelly);

            if (user == null || user.PhoneNumber != forgotPassword.PhoneNumber)
            {
                return null;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            return new ForgotPasswordResponse { Token = token };

        }

        public async Task<bool> ResetPassword(ResetPasswordRequest forgotPassword , string codeMelly)
        {
            var user = await _userManager.Users.
               FirstOrDefaultAsync(x => x.CodeMelly == codeMelly);

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(forgotPassword.Token));

            var result = await _userManager.
                ResetPasswordAsync(user, token , forgotPassword.NewPassword);

            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var user = await _currentUserServices.GetUserAsync();
            var result = await _userManager.ChangePasswordAsync(user,
                changePasswordRequest.CurrenPassword, changePasswordRequest.NewPassword);

            if(result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
