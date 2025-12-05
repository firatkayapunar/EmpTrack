using AutoMapper;
using EmpTrack.Application.Features.Employees.Dtos;
using EmpTrack.Domain.Entities;

namespace EmpTrack.Application.Features.Employees.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.Title.Name));

            CreateMap<EmployeeDto, Employee>();
        }
    }
}
