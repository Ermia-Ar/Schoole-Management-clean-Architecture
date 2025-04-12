using Core.Application.DTOs.Email;

namespace Core.Application.Interfaces.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailRequest EmailInfo);
    }
}
