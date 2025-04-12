using Core.Application.DTOs.Authorize;
using Core.Application.Featurs.Authorize.Commands;
using Core.Application.Featurs.Authorize.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using School_Management.Api.Base;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : AppControllerBase
    {
        private IMediator _mediator;

        public AuthorizeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            var request = new SignInCommand { SignInRequest = signInRequest };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpPost]
        [Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            var request = new SignOutCommand();
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var request = new GetRolesQuery();
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpGet]
        [Route("GetUserInRole")]
        public async Task<IActionResult> GetUserInRole(string roleId)
        {
            var request = new GetUserInRoleQuery() { RoleId = roleId };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpPost]
        [Route("AddRoleRoUser")]
        public async Task<IActionResult> AddRoleToUser(string roleId, string userId)
        {
            var request = new AddRoleToUserCommand { RoleId = roleId, UserId = userId };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpPost]
        [Route("UserRemoveFromRole")]
        public async Task<IActionResult> RemoveFromRole(string roleId, string userId)
        {
            var request = new DeleteFromRoleCommand { RoleId = roleId, UserId = userId };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }
    }
}
