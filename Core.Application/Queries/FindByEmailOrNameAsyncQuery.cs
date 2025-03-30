using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Queries
{
    public class FindByEmailOrNameAsyncQuery : IRequest<IdentityUser>
    {
        public string emailOrUserName { get; set; }
    }
}
