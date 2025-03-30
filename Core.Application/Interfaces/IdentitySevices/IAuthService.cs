using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.Application.DTOs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Interfaces.IdentitySevices
{
    public interface IAuthService 
    {
        Task<AuthenticationBaseResponse> SignInAsync(SignInRequest signInRequest);
        Task SignOutAsync();
        Task<AuthenticationResponse> SignUpAsync(SignUpRequest signUpRequest);
        Task<AuthenticationResponse> ChangePasswordAsync(ClaimsPrincipal user, string currentPassword, string newPassword);
        Task<AuthenticationResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
        Task<TokenRequest> GeneratePasswordResetTokenAsync(string email);
        Task<ApplicationUserDto> GetCurrentUserAsync(ClaimsPrincipal user);
        Task<TokenConfirmationResponse> GenerateTokenAsync(string EmailOrName, IList<string> roles);
        Task<TokenRequest> GenerateEmailChangeAsync(ClaimsPrincipal user, string newEmail);
        Task<AuthenticationBaseResponse> ConfirmEmailAsync(TokenConfirmationResponse emailConfirmationRequest);
        Task RefreshSignInAsync(ClaimsPrincipal user);
    }
}
