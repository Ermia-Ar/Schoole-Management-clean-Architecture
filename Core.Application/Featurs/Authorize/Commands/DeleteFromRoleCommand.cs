using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authorize.Commands
{
    public class DeleteFromRoleCommand : IRequest<Response<string>>
    {
        public string RoleId { get; set; }

        public string UserId { get; set; }
    }
}
