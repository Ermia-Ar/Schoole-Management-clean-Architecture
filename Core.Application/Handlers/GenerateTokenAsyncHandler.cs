using Core.Application.DTOs.Authontication;
using Core.Application.Featurs.Authontication.Commands;
using Core.Application.Interfaces.IdentityServices;
using MediatR;

namespace Core.Application.Handlers
{
    public class GenerateTokenAsyncHandler : IRequestHandler<GenerateTokenAsyncCommand, JwtAuthResult>
    {

        private IAuthService _authService { get; set; }

        public GenerateTokenAsyncHandler(IAuthService userService)
        {
            _authService = userService;
        }
        public async Task<JwtAuthResult> Handle(GenerateTokenAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _authService.GetJWTToken(request.Request);
        }
    }
}
