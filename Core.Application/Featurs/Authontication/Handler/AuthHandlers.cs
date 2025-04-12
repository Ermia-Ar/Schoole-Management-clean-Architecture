using Core.Application.DTOs.Authentication;
using Core.Application.Featurs.Authentication.Commands;
using Core.Application.Interfaces;
using Core.Domain.Bases;
using FluentValidation;
using MediatR;

namespace Core.Application.Featurs.Authentication.Handler
{
    public class AuthHandlers : ResponseHandler
        , IRequestHandler<LoginAsyncCommand, Response<JwtAuthResult>>
        , IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
        , IRequestHandler<GenerateTokenAsyncCommand, JwtAuthResult>
        , IRequestHandler<ForgotPasswordCommand, Response<ForgotPasswordResponse>>
        , IRequestHandler<ResetPasswordCommand, Response<string>>
        , IRequestHandler<ChangePasswordCommand, Response<string>>
    {
        private IValidator<LoginInRequest> _signInValidator { get; set; }
        private IAuthService _authService { get; set; }

        public AuthHandlers(IAuthService userService, IValidator<LoginInRequest> signInValidator)
        {
            _authService = userService;
            _signInValidator = signInValidator;
        }

        public async Task<Response<JwtAuthResult>> Handle(LoginAsyncCommand request, CancellationToken cancellationToken)
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

            if (userIdAndExpireDate.Item2 == null)
            {
                return Unauthorized<JwtAuthResult>(userIdAndExpireDate.Item1);
            }
            //Expire the refresh token
            await _authService.ExpiredRefreshToken(request.RefreshTokenRequest.RefreshToken);

            // generate new access token and refresh token 
            var (CodeMelly, ExpireDate) = userIdAndExpireDate;
            var jwtTokenResult = await _authService.GetJWTToken(CodeMelly);
            if (jwtTokenResult == null)
            {
                return BadRequest<JwtAuthResult>();
            }
            return Success(jwtTokenResult);
        }

        public async Task<JwtAuthResult> Handle(GenerateTokenAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _authService.GetJWTToken(request.CodeMelly);
        }

        public async Task<Response<ForgotPasswordResponse>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ForgotPassword(request.forgotPasswordRequest);
            if (result.Token == null)
            {
                return BadRequest<ForgotPasswordResponse>("code melly or phone number is wrong !!");
            }

            return Success(result, new { message = "user this token for set a new password" });
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ResetPassword(request.ResetPasswordRequest, request.CodeMelly);
            if (!result)
            {
                return BadRequest<string>("Token is not valid");
            }
            return Success("Success");
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ChangePassword(request.ChangePasswordRequest);
            if (!result)
            {
                return BadRequest<string>("current password is wrong");
            }
            return Success("Success");
        }
    }
}
