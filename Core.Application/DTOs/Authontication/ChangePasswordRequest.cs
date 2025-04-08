namespace Core.Application.DTOs.Authentication
{
    public class ChangePasswordRequest
    {
        public string CurrenPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmePassword { get; set; }
    }
}
