using Core.Application.DTOs.Authentication;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authentication.Commands
{
    public class ChangePasswordCommand : IRequest<Response<string>>
    {
        public ChangePasswordRequest ChangePasswordRequest { get; set; }
    }
}
