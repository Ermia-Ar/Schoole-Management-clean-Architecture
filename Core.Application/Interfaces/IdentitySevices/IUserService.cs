using Core.Application.DTOs.NewFolder;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Core.Application.Interfaces.IdentityServices
{
    public interface IUserService
    {
        
        Task<IdentityUser> FindByEmailOrNameAsync(string emailOrUsername);
        Task<IList<string>> GetRolesAsync(string  email);
    }
}
