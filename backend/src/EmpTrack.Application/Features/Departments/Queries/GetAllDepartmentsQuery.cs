using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Departments.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Queries
{
    public record GetAllDepartmentsQuery : IRequest<ServiceResult<List<DepartmentDto>>>;
}
