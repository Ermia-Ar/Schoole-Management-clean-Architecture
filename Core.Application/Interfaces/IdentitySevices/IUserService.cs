using Core.Application.DTOs.NewFolder;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Core.Application.Interfaces.IdentitySevices
{
    public interface IUserService
    {
        Task<ApplicationUserDto> FindByIdAsync(string userId);
        Task<ApplicationUserDto> FindByEmailAsync(string email);
        Task<IdentityUser> FindByEmailOrNameAsync(string emailOrUsername);
        Task<IList<string>> GetRolesAsync(string  email);
    }
}
