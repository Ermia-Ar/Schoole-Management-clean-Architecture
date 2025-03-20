using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using MediatR;

namespace Core.Application.Handlers
{
    public class ConfirmEmailAsyncHandler : IRequestHandler<ConfirmEmailAsyncCommand, AuthenticationBaseResponse>
    {
        private IAuthService _authService { get; set; }

        public ConfirmEmailAsyncHandler(IAuthService userService)
        {
            _authService = userService;
        }
        public Task<AuthenticationBaseResponse> Handle(ConfirmEmailAsyncCommand request, CancellationToken cancellationToken)
        {
            return _authService.ConfirmEmailAsync(request.emailConfirmationRequest);
        }
    }
}
