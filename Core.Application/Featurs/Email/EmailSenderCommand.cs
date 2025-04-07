using Core.Application.DTOs.Email;
using Core.Application.DTOs.NewFolder;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Email
{
    public class EmailSenderCommand : IRequest<Response<string>>
    {
        public EmailRequest EmailRequest { get; set; }
    }
}
