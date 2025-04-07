using Core.Application.DTOs.Authontication;
using MediatR;
using System.Security.Claims;

namespace Core.Application.Featurs.Authontication.Commands
{
    public class GenerateTokenAsyncCommand : IRequest<JwtAuthResult>
    {
        public string Request { get; set; }
    }
}
