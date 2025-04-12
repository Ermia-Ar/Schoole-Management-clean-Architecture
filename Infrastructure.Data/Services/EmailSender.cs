using Core.Application.DTOs.Email;
using Core.Application.Interfaces.Email;
using System.Net.Mail;

namespace Infrastructure.Data.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task<bool> SendEmailAsync(EmailRequest email)
        {
            using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587)
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("rezazadeha012", "agdovbarnhkioimo"), // UserName == Email
                EnableSsl = true
            })
            {

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress("rezazadeha012@gmail.com", "School-management"),
                    To = { email.To },
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = true,
                };

                try
                {
                    smtpServer.Send(mail);
                    await Task.CompletedTask;

                    return true;
                }
                catch (Exception ex)
                {
                    await Task.CompletedTask;
                    return false;
                }
            }
        }
    }
}
