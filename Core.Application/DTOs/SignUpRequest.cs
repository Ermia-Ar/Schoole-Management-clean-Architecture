using System.Text.Json.Serialization;
using System;
namespace Core.Application.DTOs
{
    public class SignUpRequest
    {
        [JsonPropertyName("User name")]
        public string UserName { get; set; }

        [JsonPropertyName("email")] 
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("Roles")]
        public string[] Roles { get; set; }
    }
}