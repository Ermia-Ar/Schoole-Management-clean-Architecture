using Core.Application.DTOs.Authentication;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authentication.Commands
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public RefreshTokenRequest RefreshTokenRequest { get; set; }
    }
}
