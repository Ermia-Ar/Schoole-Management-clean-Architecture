using Core.Application.DTOs.Authontication;
using Core.Application.Featurs.Authontication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login([FromBody] SignInRequest loginRequest)
        {
            //check request
            var response = new SignInAsyncCommand { SignInRequest = loginRequest };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpPost]
        [Route("RefreshToken")]
        [Authorize("Admin")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenRequest refreshTokenRequest)
        {
            var request = new RefreshTokenCommand { RefreshTokenRequest = refreshTokenRequest };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }
    }
}
