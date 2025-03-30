using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

namespace Core.Application.Handlers
{
    public class SignInAsyncHandler : IRequestHandler<SignInAsyncCommand, AuthenticationBaseResponse>
    {
        private IValidator<SignInRequest> _signInValidator { get; set; }
        private IAuthService _authService { get; set; }

        public SignInAsyncHandler(IAuthService userService, IValidator<SignInRequest> signInValidator)
        {
            _authService = userService;
            _signInValidator = signInValidator;
        }

        public async Task<AuthenticationBaseResponse> Handle(SignInAsyncCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _signInValidator.ValidateAsync(request.SignInRequest);

            if (!validationResult.IsValid)
            {
                return new AuthenticationResponse
                {
                    Succeeded = false,
                    ValidationResult = validationResult
                };
            };

            return await _authService.SignInAsync(request.SignInRequest);
        }
    }
}
