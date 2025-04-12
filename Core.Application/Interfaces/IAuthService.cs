using Core.Application.DTOs.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace Core.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignInAsync(LoginInRequest signInRequest);
        Task<ForgotPasswordResponse?> ForgotPassword(ForgotPasswordRequest forgotPassword);
        Task<bool> ChangePassword(ChangePasswordRequest changePasswordRequest);
        Task<bool> ResetPassword(ResetPasswordRequest forgotPassword, string codeMelly);
        Task<JwtAuthResult> GetJWTToken(string codeMelly);
        Task<JwtAuthResult> GetRefreshToken(string codeMelly, JwtSecurityToken JwtToken, DateTime? ExpiryDate, string refreshToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        Task<string> ValidateToken(string accessToken);
        JwtSecurityToken ReadJwtToken(string AccessToken);
        Task ExpiredRefreshToken(string refreshToken);
    }
}
