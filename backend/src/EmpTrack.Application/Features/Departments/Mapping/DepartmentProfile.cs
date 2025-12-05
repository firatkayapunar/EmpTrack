using AutoMapper;
using EmpTrack.Application.Features.Departments.Dtos;
using EmpTrack.Domain.Entities;

namespace EmpTrack.Application.Features.Departments.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<DepartmentDto, Department>();
        }
    }
}
