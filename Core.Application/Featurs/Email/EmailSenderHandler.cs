using Core.Application.Interfaces.Email;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Email
{
    public class EmailSenderHandler : ResponseHandler,
        IRequestHandler<EmailSenderCommand, Response<string>>
    {
        private IEmailSender _sender;

        public EmailSenderHandler(IEmailSender sender)
        {
            _sender = sender;
        }
        public async Task<Response<string>> Handle(EmailSenderCommand request, CancellationToken cancellationToken)
        {
            var result = await _sender.SendEmailAsync(request.EmailRequest);
            if (!result)
            {
                return BadRequest<string>("Email is invalid");
            }

            return Success("Success");
        }
    }
}
