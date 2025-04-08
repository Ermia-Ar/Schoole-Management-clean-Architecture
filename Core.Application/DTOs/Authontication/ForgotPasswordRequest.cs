namespace Core.Application.DTOs.Authentication
{
    public class ForgotPasswordRequest
    {
        public string CodeMelly { get; set; }

        public string PhoneNumber { get; set; }
    }
}
