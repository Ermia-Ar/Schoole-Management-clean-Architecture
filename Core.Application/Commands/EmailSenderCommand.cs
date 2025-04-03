using Core.Application.DTOs.NewFolder;
using MediatR;

namespace Core.Application.Commands
{
    public class EmailSenderCommand : IRequest<AuthenticationResponse>
    {
        public EmailRequest EmailRequest { get; set; }
    }
}
