using Core.Application.Interfaces;
using Core.Application.Interfaces.Email;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Email
{
    public class EmailSenderHandler : ResponseHandler,
        IRequestHandler<EmailSenderCommand, Response<string>>
    {
        private IEmailSender _sender;
        private IBackgroundJobServices _jobServices;

        public EmailSenderHandler(IEmailSender sender, IBackgroundJobServices client, IBackgroundJobServices jobServices)
        {
            _sender = sender;
            _jobServices = jobServices;
        }
        public async Task<Response<string>> Handle(EmailSenderCommand request, CancellationToken cancellationToken)
        {
            var result = _jobServices.AddEnqueue<IEmailSender>(x => x.SendEmailAsync(request.EmailRequest));

            await Task.CompletedTask;
            return Success("Success");
        }
    }
}
