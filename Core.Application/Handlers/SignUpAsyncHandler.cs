using Core.Application.Commands;
using Core.Application.Interfaces.IdentitySevices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Handlers
{
    public class SignUpAsyncHandler : IRequestHandler<SignUpAsyncCommand, IdentityResult>
    {

        private IAuthService _authService { get; set; }

        public SignUpAsyncHandler(IAuthService userService)
        {
            _authService = userService;
        }

        public async Task<IdentityResult> Handle(SignUpAsyncCommand request, CancellationToken cancellationToken)
        {

            return await _authService.SignUpAsync(request.SignUpRequest);
        }
    }
}
