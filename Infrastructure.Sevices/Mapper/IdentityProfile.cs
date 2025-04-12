using AutoMapper;
using Core.Domain.Entities;
using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Mapper
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<ApplicationUser, BaseUser>()
                .ForMember(x => x.ApplicationUserId , dex => dex.MapFrom(x => x.Id))
                .ForMember(x => x.Id , dex =>dex.Ignore())
                .ReverseMap();
        }
    }
}
