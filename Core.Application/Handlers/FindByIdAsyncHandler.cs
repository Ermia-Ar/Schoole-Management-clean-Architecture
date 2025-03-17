using Core.Application.DTOs;
using Core.Application.Interfaces.IdentitySevices;
using Core.Application.Queries;
using MediatR;

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
}
