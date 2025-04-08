using Core.Application.DTOs.Authentication;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authentication.Commands
{
    public class ForgotPasswordCommand : IRequest<Response<ForgotPasswordResponse>>
    {


        public ForgotPasswordRequest forgotPasswordRequest { get; set; }
    }
}
