using Core.Application.DTOs.Authorize;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authorize.Commands
{
    public class SignInCommand : IRequest<Response<string>>
    {
        public SignInRequest SignInRequest { get; set; }
    }
}
