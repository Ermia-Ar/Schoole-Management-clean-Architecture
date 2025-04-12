using Core.Domain.Helper;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Identity.CurrentUserServices
{
    public class CurrentUserServices : ICurrentUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;

        public CurrentUserServices(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signManager = signManager;
        }
        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims
                .SingleOrDefault(claim => claim.Type == nameof(UserClaimsModel.Id)).Value;

            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return userId;
        }

        public async Task<ApplicationUser> GetUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            { throw new UnauthorizedAccessException(); }
            return user;
        }
        public bool IsSignIn()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                return false;
            }
            var IsSignIn = _signManager.IsSignedIn(user);
            return IsSignIn;
        }
        public async Task<List<string>> GetCurrentUserRolesAsync()
        {
            var user = await GetUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
