using Infrastructure.Identity.Models;

namespace Infrastructure.Data.CurrentUserService.Abstracts
{
    public interface ICurrentUserServices
    {
        public Task<ApplicationUser> GetUserAsync();
        public Guid GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
