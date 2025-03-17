using Core.Application.Commands;
using Core.Application.Interfaces.IdentitySevices;
using MediatR;

namespace Core.Application.Handlers
{
    public class SignInAsyncHandler : IRequestHandler<SignInAsyncCommand, bool>
    {

        private IAuthService _authService { get; set; }

        public SignInAsyncHandler(IAuthService userService)
        {
            _authService = userService;
        }

        public Task<bool> Handle(SignInAsyncCommand request, CancellationToken cancellationToken)
        {
            return _authService.SignInAsync(request.SignInRequest);
        }
    }
}
