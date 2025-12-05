using AutoMapper;
using EmpTrack.Application.Features.Auth.Responses;
using EmpTrack.Domain.Entities;

namespace EmpTrack.Application.Features.Auth.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, LoginResponseDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));
        }
    }
}
