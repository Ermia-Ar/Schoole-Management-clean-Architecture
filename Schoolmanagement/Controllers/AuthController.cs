using Core.Application.DTOs.Authentication;
using Core.Application.Featurs.Authentication.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using School_Management.Api.Base;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private IMediator _mediator { get; set; }

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginInRequest loginRequest)
        {
            //check request
            var response = new LoginAsyncCommand { SignInRequest = loginRequest };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenRequest refreshTokenRequest)
        {
            var request = new RefreshTokenCommand { RefreshTokenRequest = refreshTokenRequest };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            var request = new ForgotPasswordCommand {  forgotPasswordRequest  = forgotPassword };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest forgotPassword , string codeMelly)
        {
            var request = new ResetPasswordCommand { ResetPasswordRequest = forgotPassword , CodeMelly = codeMelly};
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var request = new ChangePasswordCommand { ChangePasswordRequest = changePasswordRequest };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }
    }
}
