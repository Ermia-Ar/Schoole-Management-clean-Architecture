using Core.Application.DTOs.Authentication;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authentication.Commands
{
    public class SignInAsyncCommand : IRequest<Response<JwtAuthResult>>
    {
        public SignInRequest SignInRequest { get; set; }
    }
}
