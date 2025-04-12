using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.CurrentUserServices
{
    public interface ICurrentUserServices
    {
        public Task<ApplicationUser> GetUserAsync();
        public string GetUserId();
        bool IsSignIn();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
