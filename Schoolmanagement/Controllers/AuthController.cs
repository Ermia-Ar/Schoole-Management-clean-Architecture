using AutoMapper;
using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.Handlers;
using Core.Application.Interfaces.IdentitySevices;
using Core.Application.Queries;
using Infrastructure.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator { get; set; }

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] SignUpRequest request)
        {
            var response = new SignUpAsyncCommand { SignUpRequest = request };
            var result = await _mediator.Send(response);    

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User was registered");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] SignInRequest loginRequest)
        {
            //check request
            var response = new SignInAsyncCommand { SignInRequest = loginRequest };
            var result = await _mediator.Send(response);

            if (!result.Succeeded)
            {
                return BadRequest("Username or password incorrect");
            }


            // Get Roles for this user
            var responseGet = new GetRolesAsyncQuery { usernameOrName = loginRequest.EmailOrUsername };
            var roles = await _mediator.Send(responseGet);

            //Generate Token
            if (roles != null)
            {
                var tokenRequest = new GenerateTokenAsyncCommand
                {
                    Request = new TokenRequest
                    {
                        Roles = roles,
                        EmailOrName = loginRequest.EmailOrUsername
                    }
                };
                var tokenConfirmationResponse = await _mediator.Send(tokenRequest);

                return Ok(new LoginResponse() { Token = tokenConfirmationResponse.Token });
            }

            return BadRequest("Username or password incorrect");

        }
    }
}
