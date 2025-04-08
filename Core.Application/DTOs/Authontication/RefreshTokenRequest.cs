namespace Core.Application.DTOs.Authentication
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; } 

        public string RefreshToken { get; set; }
    }
}
