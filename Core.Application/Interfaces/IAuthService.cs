using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Core.Application.DTOs.Authontication;
using System.IdentityModel.Tokens.Jwt;

namespace Core.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignInAsync(SignInRequest signInRequest);
        Task<JwtAuthResult> GetJWTToken(string codeMelly);
        Task<JwtAuthResult> GetRefreshToken(string codeMelly, JwtSecurityToken JwtToken, DateTime? ExpiryDate, string refreshToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        Task<string> ValidateToken(string accessToken);
        JwtSecurityToken ReadJwtToken(string AccessToken);
        Task ExpiredRefreshToken(string refreshToken);
    }
}
