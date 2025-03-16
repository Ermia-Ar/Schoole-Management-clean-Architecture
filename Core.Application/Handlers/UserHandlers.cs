using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using Core.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Handlers
{
    public class UserHandlers : IRequestHandler<FindByIdAsyncQuery, ApplicationUserDto>
    {
        private IUserService _userService { get; set; }

        public UserHandlers(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<ApplicationUserDto> Handle(FindByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _userService.FindByIdAsync(request.userId);
        }
    }
}
