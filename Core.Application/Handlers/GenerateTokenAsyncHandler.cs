using Core.Application.Commands;
using Core.Application.DTOs.NewFolder;
using Core.Application.Interfaces.IdentitySevices;
using MediatR;

namespace Core.Application.Handlers
{
    public class GenerateTokenAsyncHandler : IRequestHandler<GenerateTokenAsyncCommand, TokenConfirmationResponse>
    {

        private IAuthService _authService { get; set; }

        public GenerateTokenAsyncHandler(IAuthService userService)
        {
            _authService = userService;
        }
        public async Task<TokenConfirmationResponse> Handle(GenerateTokenAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _authService.GenerateTokenAsync(request.Request.EmailOrName , request.Request.Roles);
        }
    }
}
