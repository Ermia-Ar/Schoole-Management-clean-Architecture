using MediatR;

namespace Core.Application.Queries
{
    public class GetRolesAsyncQuery : IRequest<IList<string>>
    {
        public string usernameOrName { get; set; }
    }
}
