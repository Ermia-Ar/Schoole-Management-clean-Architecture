using Core.Application.DTOs.Authontication;
using Core.Application.Featurs.Authontication.Commands;
using Core.Application.Interfaces;
using Core.Domain.Bases;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

namespace Core.Application.Featurs.Authontication.Handler
{
    public class AuthHandlers : ResponseHandler
        , IRequestHandler<SignInAsyncCommand, Response<JwtAuthResult>>
        , IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
        , IRequestHandler<GenerateTokenAsyncCommand, JwtAuthResult>
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
            if (!result)
            {
                return BadRequest<JwtAuthResult>("user name or password is not correct!");
            }
            var jwtTokenResult = await _authService.GetJWTToken(request.SignInRequest.CodeMelly);

            return Success(jwtTokenResult);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //check token validation
            var jwtToken = _authService.ReadJwtToken(request.RefreshTokenRequest.AccessToken);
            var userIdAndExpireDate = await _authService.ValidateDetails(jwtToken,
                request.RefreshTokenRequest.AccessToken, request.RefreshTokenRequest.RefreshToken);

            if(userIdAndExpireDate.Item2 == null)
            {
                return Unauthorized<JwtAuthResult>(userIdAndExpireDate.Item1);
            }
            //Expire the refresh token
            await _authService.ExpiredRefreshToken(request.RefreshTokenRequest.RefreshToken);

            // generate new access token and refresh token 
            var (CodeMelly, ExpireDate) = userIdAndExpireDate;
            var jwtTokenResult = await _authService.GetJWTToken(CodeMelly);
            if(jwtTokenResult == null)
            {
                return BadRequest<JwtAuthResult>();
            }
            return Success(jwtTokenResult);
        }

        public async Task<JwtAuthResult> Handle(GenerateTokenAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _authService.GetJWTToken(request.CodeMelly);
        }
    }
}
