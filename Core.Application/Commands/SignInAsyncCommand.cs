using Core.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Commands
{
    public class SignInAsyncCommand : IRequest<AuthenticationResponse>
    {
        public SignInRequest SignInRequest { get; set; }
    }
}
