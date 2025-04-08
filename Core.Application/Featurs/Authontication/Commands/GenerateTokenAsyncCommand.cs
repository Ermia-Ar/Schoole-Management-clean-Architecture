using Core.Application.DTOs.Authentication;
using MediatR;
using System.Security.Claims;

namespace Core.Application.Featurs.Authentication.Commands
{
    public class GenerateTokenAsyncCommand : IRequest<JwtAuthResult>
    {
        public string CodeMelly { get; set; }
    }
}
