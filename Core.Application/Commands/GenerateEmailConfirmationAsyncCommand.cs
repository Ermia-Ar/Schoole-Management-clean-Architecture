using Core.Application.DTOs;
using MediatR;
using System.Security.Claims;

namespace Core.Application.Commands
{
    public class GenerateEmailConfirmationAsyncCommand : IRequest<TokenResponse>
    {
        public  ClaimsPrincipal user { get; set; }
    }
}
