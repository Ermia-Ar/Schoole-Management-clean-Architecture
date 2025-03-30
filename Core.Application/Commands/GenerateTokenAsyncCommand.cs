using Core.Application.DTOs;
using MediatR;
using System.Security.Claims;

namespace Core.Application.Commands
{
    public class GenerateTokenAsyncCommand : IRequest<TokenConfirmationResponse>
    { 
        public TokenRequest Request { get; set; }
    }
}
