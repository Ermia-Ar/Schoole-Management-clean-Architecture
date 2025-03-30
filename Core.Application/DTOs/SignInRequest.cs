using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class SignInRequest
    {
        [JsonPropertyName("email")]
        public string EmailOrUsername { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

    }
}