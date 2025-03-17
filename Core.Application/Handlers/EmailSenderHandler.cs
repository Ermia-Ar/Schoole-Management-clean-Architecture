using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.Interfaces.Email;
using MediatR;

namespace Core.Application.Handlers
{
    public class EmailSenderHandler : IRequestHandler<EmailSenderCommand, AuthenticationResponse>
    {
        private IEmailSender _sender;

        public EmailSenderHandler(IEmailSender sender)
        {
            _sender = sender;
        }
        public async Task<AuthenticationResponse> Handle(EmailSenderCommand request, CancellationToken cancellationToken)
        {
            return await _sender.SendEmailAsync(request.EmailRequest);
        }
    }
}
