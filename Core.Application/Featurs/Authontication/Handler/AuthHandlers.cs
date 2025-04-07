using Core.Application.DTOs.Authontication;
using Core.Application.Featurs.Authontication.Commands;
using Core.Application.Interfaces.IdentityServices;
using Core.Domain.Bases;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

namespace Core.Application.Featurs.Authontication.Handler
{
    public class AuthHandlers : ResponseHandler
        , IRequestHandler<SignInAsyncCommand, Response<JwtAuthResult>>
        , IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {
        private IValidator<SignInRequest> _signInValidator { get; set; }
        private IAuthService _authService { get; set; }

        public AuthHandlers(IAuthService userService, IValidator<SignInRequest> signInValidator)
        {
            _authService = userService;
            _signInValidator = signInValidator;
        }

        public async Task<Response<JwtAuthResult>> Handle(SignInAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.SignInAsync(request.SignInRequest);
            if (!result.Succeeded)
            {
                return BadRequest<JwtAuthResult>(result.Errors[0]);
            }
            var jwtTokenResult = await _authService.GetJWTToken(request.SignInRequest.CodeMelly);

            return Success(jwtTokenResult);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authService.ReadJwtToken(request.RefreshTokenRequest.AccessToken);
            var userIdAndExpireDate = await _authService.ValidateDetails(jwtToken,
                request.RefreshTokenRequest.AccessToken, request.RefreshTokenRequest.RefreshToken);

            if(userIdAndExpireDate.Item2 == null)
            {
                return Unauthorized<JwtAuthResult>(userIdAndExpireDate.Item1);
            }

            var (CodeMelly, ExpireDate) = userIdAndExpireDate;

            //Expire the refresh token
            await _authService.ExpiredRefreshToken(request.RefreshTokenRequest.RefreshToken);

            var jwtTokenResult = await _authService.GetJWTToken(CodeMelly);
            if(jwtTokenResult == null)
            {
                return BadRequest<JwtAuthResult>();
            }
            return Success(jwtTokenResult);
        }
    }
}
