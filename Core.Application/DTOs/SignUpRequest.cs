using System.Text.Json.Serialization;
using System;
namespace Core.Application.DTOs
{
    public class SignUpRequest
    {
        [JsonPropertyName("اسم کامل")]
        public string UserName { get; set; }

        [JsonPropertyName("کد ملی")] 
        public string CodeMelly { get; set; }

        [JsonPropertyName("رمز ورورد")]
        public string Password { get; set; }

        [JsonPropertyName("نقش")]
        public string[] Roles { get; set; }
    }
}