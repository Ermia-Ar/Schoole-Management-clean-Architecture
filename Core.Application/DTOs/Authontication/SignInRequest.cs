using System.Text.Json.Serialization;

namespace Core.Application.DTOs.Authontication
{
    public class SignInRequest
    {
        public string CodeMelly { get; set; }

        public string Password { get; set; }

    }
}