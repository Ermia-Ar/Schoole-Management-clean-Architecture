using AutoMapper;
using Core.Application.DTOs.NewFolder;
using Core.Application.Interfaces.IdentitySevices;
using Infrastructure.Identity.Models;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
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

namespace Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        //private SignInManager<IdentityUser> _signInManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IConfiguration _configuration { get; set; }
        private IMapper _mapper { get; set; }

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            //_signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
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

        public async Task<AuthenticationBaseResponse> SignInAsync(SignInRequest signInRequest)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.CodeMelly == signInRequest.CodeMelly);
            //check email or user name
            if (user == null)
            {
                return new AuthenticationBaseResponse { Succeeded = false , 
                    Errors = new List<string> { "User not found !" } };
            }
            // check password 
            bool checkPassword = await _userManager.CheckPasswordAsync(user, signInRequest.Password); 
            if(!checkPassword)
            {
                return new AuthenticationBaseResponse { Succeeded = false , 
                    Errors = new List<string> { "user name or password is not correct !" } };
            }

            return new AuthenticationBaseResponse { Succeeded = true };

        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResponse> SignUpAsync(SignUpRequest signUpRequest)
        {
            //add to asp user table 
            var user = new ApplicationUser
            {
                CodeMelly = signUpRequest.CodeMelly,
                FullName = signUpRequest.FullName,
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

            //add to role table
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
