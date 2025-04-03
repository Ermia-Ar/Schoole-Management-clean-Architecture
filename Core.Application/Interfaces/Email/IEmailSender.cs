using Core.Application.DTOs.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Email
{
    public interface IEmailSender
    {
        Task<AuthenticationResponse> SendEmailAsync(EmailRequest EmailInfo);
    }
}
