using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Core.Application.DTOs.NewFolder;
using Core.Application.DTOs.Authontication;

namespace Core.Application.Interfaces.IdentityServices
{
    public interface IAuthService : IAuthenticationServices
    {
        Task<AuthenticationBaseResponse> SignInAsync(SignInRequest signInRequest);
    }
}
