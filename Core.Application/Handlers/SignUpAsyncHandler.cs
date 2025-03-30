using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Handlers
{
    public class SignUpAsyncHandler : IRequestHandler<SignUpAsyncCommand, AuthenticationResponse>
    {
        private IValidator<SignUpRequest> _signUpValidator { get; set; }
        private IAuthService _authService { get; set; }

        public SignUpAsyncHandler(IAuthService userService, IValidator<SignUpRequest> signUpValidator)
        {
            _authService = userService;
            _signUpValidator = signUpValidator;
        }

        public async Task<AuthenticationResponse> Handle(SignUpAsyncCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _signUpValidator.ValidateAsync(request.SignUpRequest);
            if (!validationResult.IsValid)
            {
                return new AuthenticationResponse
                {
                    Succeeded = false,
                    ValidationResult = validationResult
                };
            }

            return await _authService.SignUpAsync(request.SignUpRequest);
        }
    }
}
