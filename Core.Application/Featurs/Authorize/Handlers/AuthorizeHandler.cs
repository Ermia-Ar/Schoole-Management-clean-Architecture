using Core.Application.Featurs.Authorize.Commands;
using Core.Application.Featurs.Authorize.Queries;
using Core.Application.Interfaces;
using Core.Domain.Bases;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Featurs.Authorize.Handlers
{
    public class AuthorizeHandler : ResponseHandler
        , IRequestHandler<SignInCommand, Response<string>>
        , IRequestHandler<SignOutCommand, Response<string>>
        , IRequestHandler<GetRolesQuery, Response<List<string>>>
        , IRequestHandler<AddRoleToUserCommand, Response<string>>
        , IRequestHandler<DeleteFromRoleCommand, Response<string>>
        , IRequestHandler<GetUserInRoleQuery, Response<List<BaseUser>>>
    {
        public IAuthorizeServices _services { get; set; }

        public AuthorizeHandler(IAuthorizeServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.SignIn(request.SignInRequest);

            if (!result)
            {
                return BadRequest<string>("User name or password is wrong");
            }
            return Success<string>("Success");
        }

        public async Task<Response<string>> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.SignOut();
            if (result)
            {
                return Success<string>("Success");
            }
            return Unauthorized<string>("first login");
        }

        public async Task<Response<List<string>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetRoles();
            return Success(result);
        }

        public async Task<Response<string>> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.AddRoleToUser(request.UserId, request.RoleId);
            if (!result)
            {
                return BadRequest<string>("User id or Role id is wrong");
            }
            return Success<string>("Success"); 
        }

        public async Task<Response<string>> Handle(DeleteFromRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.RemoveRoleFromUser(request.UserId, request.RoleId);
            if (!result)
            {
                return BadRequest<string>("User id or Role id is Not valid");
            }
            return Success<string>("Success");
        }

        public async Task<Response<List<BaseUser>>> Handle(GetUserInRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetUserInRole(request.RoleId);
            return Success(result);
        }
    }
}
