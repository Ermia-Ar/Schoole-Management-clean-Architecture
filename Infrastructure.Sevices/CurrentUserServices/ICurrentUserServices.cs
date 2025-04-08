using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.CurrentUserServices
{
    public interface ICurrentUserServices
    {
        public Task<ApplicationUser> GetUserAsync();
        public string GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
