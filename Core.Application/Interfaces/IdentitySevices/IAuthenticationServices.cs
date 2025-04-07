using Core.Application.DTOs.Authontication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.IdentityServices
{
    public interface IAuthenticationServices    
    {
        public Task<JwtAuthResult> GetJWTToken(string codeMelly);
        public Task<JwtAuthResult> GetRefreshToken(string codeMelly, JwtSecurityToken JwtToken, DateTime? ExpiryDate, string refreshToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        public Task<string> ValidateToken(string accessToken);
        public JwtSecurityToken ReadJwtToken(string AccessToken);
        public Task ExpiredRefreshToken(string refreshToken);
    }
}
