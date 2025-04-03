using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Core.Application.DTOs.NewFolder
{
    public class AuthenticationResponse : AuthenticationBaseResponse
    {
        public ValidationResult ValidationResult { get; set; }
    }
}
