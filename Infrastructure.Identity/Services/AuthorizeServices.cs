using AutoMapper;
using Core.Application.DTOs.Authorize;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Identity.CurrentUserServices;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Services
{
    public class AuthorizeServices : IAuthorizeServices
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ICurrentUserServices _userServices;
        private IMapper _mapper;


        public AuthorizeServices(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            ICurrentUserServices userServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userServices = userServices;
        }

        public async Task<bool> AddRoleToUser(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.AddToRoleAsync(user, roleId);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<List<string>> GetRoles()
        {
            var roles = await _roleManager.Roles
                .Select(x => x.Name).ToListAsync();
            return roles;
        }

        public async Task<List<BaseUser>> GetUserInRole(string RoleId)
        {
            var result = await _userManager.GetUsersInRoleAsync(RoleId);

            var users = _mapper.Map<List<BaseUser>>(result);

            return users;
        }

        public async Task<List<string>?> GetUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> RemoveRoleFromUser(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.RemoveFromRoleAsync(user, roleId);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SignIn(SignInRequest signInRequest)
        {
            //check user name is exist 
            var user = await _userManager.FindByNameAsync(signInRequest.UserName);
            if (user == null)
            {
                return false;
            }
            //add to user logins table
            var signInResult = await _signInManager.PasswordSignInAsync(
                signInRequest.UserName, signInRequest.Password, signInRequest.RememberMe, true);

            if (!signInResult.Succeeded)
            {
                return false;
            }


            return true;
        }

        public async Task<bool> SignOut()
        {
            var isSignIn = _userServices.IsSignIn();
            if (!isSignIn)
            {
                return false;
            }
            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
