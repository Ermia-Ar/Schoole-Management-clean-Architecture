using Infrastructure.Identity.Models;

namespace Infrastructure.Data.CurrentUserServices.Abstracts
{
    public interface ICurrentUserServices
    {
        public Task<ApplicationUser> GetUserAsync();
        public Guid GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
