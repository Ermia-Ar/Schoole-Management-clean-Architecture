using Infrastructure.Data.Interfaces;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> CreateUserAsync(ApplicationUser user, string password , params string[] roles)
        {
            var result = await _userManager.CreateAsync(user , password);
            if(!result.Succeeded)
                return result.Succeeded;

            result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsyncById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            // remove from user table
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddToRoles(ApplicationUser user ,params string[] roles)
        {
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }
    }
}
