using Infrastructure.Identity.Models;

namespace Infrastructure.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> DeleteUserAsyncById(string id);
        Task<bool> CreateUserAsync(ApplicationUser user, string password, params string[] roles);
        Task<bool> AddToRoles(ApplicationUser user ,params string[] roles);
    }
}
