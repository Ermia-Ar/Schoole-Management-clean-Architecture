using Core.Application.DTOs.Email;
using Core.Application.Featurs.Email;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management.Api.Base;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : AppControllerBase
    {
        IMediator _mediator { get; set; }

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail([FromForm]EmailRequest emailRequest)
        {
            var request  = new EmailSenderCommand {  EmailRequest = emailRequest };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }
    }
}
