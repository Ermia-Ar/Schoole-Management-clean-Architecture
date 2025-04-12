using Core.Domain.Bases;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Featurs.Authorize.Queries
{
    public class GetUserInRoleQuery : IRequest<Response<List<BaseUser>>>
    {
        public string RoleId { get; set; }
    }
}
