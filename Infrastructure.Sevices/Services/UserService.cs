using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
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
        public Task<AuthenticationResponse> AddPasswordAsync(ClaimsPrincipal principal, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> ChangeEmailAsync(ClaimsPrincipal principal, string email, string code)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUserDto> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUserDto> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumberAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailConfirmedAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> SetPhoneNumberAsync(ClaimsPrincipal principal, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
