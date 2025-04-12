using Core.Application.DTOs.Authentication;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authentication.Commands
{
    public class LoginAsyncCommand : IRequest<Response<JwtAuthResult>>
    {
        public LoginInRequest SignInRequest { get; set; }
    }
}
