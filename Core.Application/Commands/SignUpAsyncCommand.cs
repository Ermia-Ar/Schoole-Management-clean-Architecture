using Core.Application.DTOs.NewFolder;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Commands
{
    public class SignUpAsyncCommand : IRequest<AuthenticationResponse>
    {
        public SignUpRequest SignUpRequest { get; set; }
    }
}
