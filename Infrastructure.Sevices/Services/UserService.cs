using Core.Application.DTOs.NewFolder;
using Core.Application.Interfaces.IdentitySevices;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        //private SignInManager<IdentityUser> _signInManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }

        public UserService( UserManager<ApplicationUser> userManager)
        {
            //_signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityUser> FindByEmailOrNameAsync(string emailOrUsername)
        {
            var Email = await _userManager.FindByEmailAsync(emailOrUsername);
            if (Email != null)
            {
                return Email;
            }

            var Name = await _userManager.FindByNameAsync(emailOrUsername);
            if (Name != null)
            {
                return Name;
            }

            return null;
        }
     
        public async Task<IList<string>> GetRolesAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public Task<ApplicationUserDto> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUserDto> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
