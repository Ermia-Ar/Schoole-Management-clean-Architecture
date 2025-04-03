using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Core.Application.DTOs.NewFolder;

namespace Core.Application.Interfaces.IdentitySevices
{
    public interface IAuthService 
    {
        Task<AuthenticationBaseResponse> SignInAsync(SignInRequest signInRequest);
        Task SignOutAsync();
        Task<AuthenticationResponse> SignUpAsync(SignUpRequest signUpRequest);
        Task<TokenConfirmationResponse> GenerateTokenAsync(string EmailOrName, IList<string> roles);
        Task<AuthenticationBaseResponse> ConfirmEmailAsync(TokenConfirmationResponse emailConfirmationRequest);
    }
}
