using Core.Application.DTOs.Authontication;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Authontication.Commands
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public RefreshTokenRequest RefreshTokenRequest { get; set; }
    }
}
