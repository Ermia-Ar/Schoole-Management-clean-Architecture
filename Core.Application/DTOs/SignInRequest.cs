using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class SignInRequest
    {
        [JsonPropertyName("کد ملی")]
        public string CodeMelly { get; set; }

        [JsonPropertyName("رمز ورورد")]
        public string Password { get; set; }

    }
}