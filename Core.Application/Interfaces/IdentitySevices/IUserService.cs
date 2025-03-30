using Core.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Core.Application.Interfaces.IdentitySevices
{
    public interface IUserService
    {
        Task<ApplicationUserDto> FindByIdAsync(string userId);
        Task<ApplicationUserDto> FindByEmailAsync(string email);
        Task<IdentityUser> FindByEmailOrNameAsync(string emailOrUsername);
        Task<string> GetUserIdAsync(ClaimsPrincipal principal);
        Task<string> GetEmailAsync(ClaimsPrincipal principal);
        Task<string> GetUserNameAsync(ClaimsPrincipal principal);
        Task<string> GetPhoneNumberAsync(ClaimsPrincipal principal);
        Task<AuthenticationResponse> ChangeEmailAsync(ClaimsPrincipal principal, string email, string code);
        Task<AuthenticationResponse> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword);
        Task<bool> IsEmailConfirmedAsync(string email);
        Task<bool> HasPasswordAsync(ClaimsPrincipal principal);
        Task<AuthenticationResponse> SetPhoneNumberAsync(ClaimsPrincipal principal, string phoneNumber);
        Task<AuthenticationResponse> AddPasswordAsync(ClaimsPrincipal principal, string newPassword);
        Task<IList<string>> GetRolesAsync(string  email);
    }
}
