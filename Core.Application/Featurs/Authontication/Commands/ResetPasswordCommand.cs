using Core.Application.DTOs.Authentication;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authentication.Commands
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string CodeMelly { get; set; }

        public ResetPasswordRequest ResetPasswordRequest { get; set; }
    }
}
