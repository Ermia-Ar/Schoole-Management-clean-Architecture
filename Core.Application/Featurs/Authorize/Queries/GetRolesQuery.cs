using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authorize.Queries
{
    public class GetRolesQuery : IRequest<Response<List<string>>>
    {

    }
}
