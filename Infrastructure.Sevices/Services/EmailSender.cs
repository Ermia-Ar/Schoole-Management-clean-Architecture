using Core.Application.DTOs.NewFolder;
using Core.Application.Interfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task<AuthenticationResponse> SendEmailAsync(EmailRequest email)
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
                    From = new MailAddress("rezazadeha012@gmail.com", "Appication"),
                    To = { email.To },
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = true,
                };

                try
                {
                    smtpServer.Send(mail);
                    await Task.CompletedTask;

                    return new AuthenticationResponse { Succeeded = true };
                }
                catch (Exception ex)
                {
                    await Task.CompletedTask;
                    return new AuthenticationResponse { Succeeded = false, Errors = new List<string> { "1", "Email is invalid." } };
                }
            }
        }
    }
}
