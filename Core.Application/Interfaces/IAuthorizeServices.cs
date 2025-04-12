using Core.Application.DTOs.Authorize;
using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
    public interface IAuthorizeServices
    {
        public Task<bool> SignIn(SignInRequest signInRequest);

        public Task<bool> SignOut();

        public Task<List<string>> GetRoles();

        public Task<List<string>?> GetUserRoles(string id);

        public Task<bool> AddRoleToUser(string userId , string roleId);

        public Task<bool> RemoveRoleFromUser(string userId , string roleId);

        public Task<List<BaseUser>> GetUserInRole(string roleId);
         
    }
}
