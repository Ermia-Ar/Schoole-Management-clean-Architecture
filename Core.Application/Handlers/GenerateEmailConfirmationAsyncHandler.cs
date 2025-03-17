using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using MediatR;

namespace Core.Application.Handlers
{
    public class GenerateEmailConfirmationAsyncHandler : IRequestHandler<GenerateEmailConfirmationAsyncCommand, TokenResponse>
    {

        private IAuthService _authService { get; set; }

        public GenerateEmailConfirmationAsyncHandler(IAuthService userService)
        {
            _authService = userService;
        }
        public async Task<TokenResponse> Handle(GenerateEmailConfirmationAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _authService.GenerateEmailConfirmationAsync(request.user);
        }
    }
}
