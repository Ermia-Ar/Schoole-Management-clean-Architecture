using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using Core.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Handlers
{
    public class FindByIdAsyncHandler : IRequestHandler<FindByIdAsyncQuery, ApplicationUserDto>
    {
        private IUserService _userService { get; set; }

        public FindByIdAsyncHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<ApplicationUserDto> Handle(FindByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _userService.FindByIdAsync(request.userId);
        }
    }

    public class SignInAsyncHandler : IRequestHandler<SignInAsyncCommand, bool>
    {

        private IAuthService _authService { get; set; }

        public SignInAsyncHandler(IAuthService userService)
        {
            _authService = userService;
        }

        public Task<bool> Handle(SignInAsyncCommand request, CancellationToken cancellationToken)
        {
            return _authService.SignInAsync(request.SignInRequest);
        }
    }

    public class SignUpAsyncHandler : IRequestHandler<SignUpAsyncCommand, IdentityResult>
    {

        private IAuthService _authService { get; set; }

        public SignUpAsyncHandler(IAuthService userService)
        {
            _authService = userService;
        }

        public async Task<IdentityResult> Handle(SignUpAsyncCommand request, CancellationToken cancellationToken)
        {

            return await _authService.SignUpAsync(request.SignUpRequest);
        }
    }
}
