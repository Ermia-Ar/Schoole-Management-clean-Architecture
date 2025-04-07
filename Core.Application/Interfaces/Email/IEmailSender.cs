using Core.Application.DTOs.Authontication;
using Core.Application.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailRequest EmailInfo);
    }
}
