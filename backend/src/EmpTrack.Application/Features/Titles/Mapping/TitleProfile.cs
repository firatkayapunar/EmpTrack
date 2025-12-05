using AutoMapper;
using EmpTrack.Application.Features.Titles.Dtos;
using EmpTrack.Domain.Entities;

namespace EmpTrack.Application.Features.Titles.Mapping
{
    public class TitleProfile : Profile
    {
        public TitleProfile()
        {
            CreateMap<Title, TitleDto>();

            CreateMap<TitleDto, Title>();
        }
    }
}
