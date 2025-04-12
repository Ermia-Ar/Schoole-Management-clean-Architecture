using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authorize.Commands
{
    public class SignOutCommand : IRequest<Response<string>>
    {
    }
}
