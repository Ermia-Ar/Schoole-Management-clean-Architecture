using Core.Application.DTOs.Authontication;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authontication.Commands
{
    public class SignInAsyncCommand : IRequest<Response<JwtAuthResult>>
    {
        public SignInRequest SignInRequest { get; set; }
    }
}
