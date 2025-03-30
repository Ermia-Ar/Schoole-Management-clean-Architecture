using Core.Application.Interfaces.IdentitySevices;
using Core.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Handlers
{
    public class FindByEmailOrNameAsyncHandler : IRequestHandler<FindByEmailOrNameAsyncQuery, IdentityUser>
    {
        private readonly IUserService _userService;

        public FindByEmailOrNameAsyncHandler(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<IdentityUser> Handle(FindByEmailOrNameAsyncQuery request, CancellationToken cancellationToken)
        {
            if (request.emailOrUserName == null)
            {
                throw new Exception();
            }
            return await _userService.FindByEmailOrNameAsync(request.emailOrUserName);
        }
    }
}
