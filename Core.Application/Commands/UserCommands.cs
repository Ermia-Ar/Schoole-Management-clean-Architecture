using Core.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Commands
{
    public class SignInAsyncCommand : IRequest<bool>
    {
        public SignInRequest SignInRequest { get; set; }
    }

    public class SignUpAsyncCommand : IRequest<IdentityResult>
    {
        public SignUpRequest SignUpRequest { get; set; }
    }

}
