using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using Infrastructure.Identity.Models;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        //private SignInManager<IdentityUser> _signInManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IConfiguration _configuration { get; set; }

        public AuthService( UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            //_signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public Task<AuthenticationResponse> ChangePasswordAsync(ClaimsPrincipal user, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationBaseResponse> ConfirmEmailAsync(TokenConfirmationResponse emailConfirmationRequest)
        {
            var user = await _userManager.FindByIdAsync(emailConfirmationRequest.UserId);
            if (user == null)
            {
                return new AuthenticationBaseResponse
                {
                    Succeeded = false,
                    Errors = new List<string> { "Invalid Request" }
                };
            }
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(emailConfirmationRequest.Token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {

                return new AuthenticationBaseResponse
                {
                    Succeeded = true,
                };
            }
            return new AuthenticationBaseResponse
            {
                Succeeded = false,
                Errors = new List<string> { "Invalid Request" }
            };
        }

        public Task<TokenRequest> GenerateEmailChangeAsync(ClaimsPrincipal user, string newEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenConfirmationResponse> GenerateTokenAsync(string EmailOrName, IList<string> roles)
        {
            //find user 
            var user = await FindByEmailOrUsername(EmailOrName);
            // Create claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            foreach (var role in roles.ToList())
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new TokenConfirmationResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) , UserId = user.Id};
        }

        public Task<TokenRequest> GeneratePasswordResetTokenAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUserDto> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task RefreshSignInAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationBaseResponse> SignInAsync(SignInRequest signInRequest)
        {
            var user = await FindByEmailOrUsername(signInRequest.CodeMelly);
            //check email or user name
            if (user == null)
            {
                return new AuthenticationBaseResponse { Succeeded = false };
            }
            // check password 
            bool checkPassword = await _userManager.CheckPasswordAsync(user, signInRequest.Password); 
            if(!checkPassword)
            {
                return new AuthenticationBaseResponse { Succeeded = false };
            }

            return new AuthenticationBaseResponse { Succeeded = true };

        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResponse> SignUpAsync(SignUpRequest signUpRequest)
        {
            var user = new ApplicationUser
            {
                Email = signUpRequest.CodeMelly,
                UserName = signUpRequest.UserName,
            };
            var result = await _userManager.CreateAsync(user, signUpRequest.Password);
            if (!result.Succeeded)
            {
                return new AuthenticationResponse
                {
                    Succeeded = result.Succeeded,
                    Errors = result.Errors.Select(x => x.Description).ToList()
                };
            }

            result = await _userManager.AddToRolesAsync(user, signUpRequest.Roles);

            return new AuthenticationResponse
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(x => x.Description).ToList()
            };
        }
        public async Task<ApplicationUser?>  FindByEmailOrUsername(string emailOrUSerName)
        {
            var Email = await _userManager.FindByEmailAsync(emailOrUSerName);
            if (Email != null)
            {
                return Email;
            }

            var Name = await _userManager.FindByNameAsync(emailOrUSerName);
            if(Name != null)
            {
                return Name;
            }

            return null;    
        }
    }
}
