using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
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
        private SignInManager<IdentityUser> _signInManager { get; set; }
        private UserManager<IdentityUser> _userManager { get; set; }

        public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public Task<AuthenticationResponse> ChangePasswordAsync(ClaimsPrincipal user, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResponse> ConfirmEmailAsync(EmailConfirmationRequest emailConfirmationRequest)
        {
            var user = await _userManager.FindByIdAsync(emailConfirmationRequest.UserId);
            if (user == null)
            {
                return new AuthenticationResponse
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string> { { "1", "Invalid Request" } }
                };
            }
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(emailConfirmationRequest.Token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {

                return new AuthenticationResponse
                {
                    Succeeded = true,
                };
            }
            return new AuthenticationResponse
            {
                Succeeded = false,
                Errors = new Dictionary<string, string> { { "1", "Invalid Request" } }
            };
        }

        public Task<TokenResponse> GenerateEmailChangeAsync(ClaimsPrincipal user, string newEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponse> GenerateEmailConfirmationAsync(ClaimsPrincipal principal)
        {
            var User = await _userManager.GetUserAsync(principal);
            if (User == null)
            {
                return new TokenResponse()
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string>() { { string.Empty, "Invalid request." } }
                };
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);
            return new TokenResponse
            {
                Succeeded = true,
                UserId = User.Id,
                Token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token))
            };
        }

        public Task<TokenResponse> GeneratePasswordResetTokenAsync(string email)
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

        public async Task<bool> SignInAsync(SignInRequest signInRequest)
        {
            var user = new IdentityUser();
            var isEmail = (await _userManager.FindByEmailAsync(signInRequest.Email_UserName)) == null ? false : true;
            if (isEmail)
                user.Email = signInRequest.Email_UserName;
            else
                user.UserName = signInRequest.Email_UserName;


            var result = await _signInManager.PasswordSignInAsync(user, signInRequest.Password, signInRequest.RememberMe, false);
            return result.Succeeded;
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> SignUpAsync(SignUpRequest signUpRequest)
        {
            var user = new IdentityUser
            {
                Email = signUpRequest.Email,
                UserName = signUpRequest.UserName,
            };
            var result = await _userManager.CreateAsync(user, signUpRequest.Password);

            return result;
        }
    }
}
