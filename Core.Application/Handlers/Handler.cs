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
    public class GetRolesAsyncHandler : IRequestHandler<GetRolesAsyncQuery, IList<string>>
    {
        private readonly IUserService _userService;

        public GetRolesAsyncHandler(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<IList<string>> Handle(GetRolesAsyncQuery request, CancellationToken cancellationToken)
        {
            if(request.usernameOrName == null)
            {
                throw new Exception();
            }

            return await _userService.GetRolesAsync(request.usernameOrName);
        }
    }
}
