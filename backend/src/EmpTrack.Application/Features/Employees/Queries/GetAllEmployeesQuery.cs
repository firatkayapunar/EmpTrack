using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Queries
{
    public record GetAllEmployeesQuery : IRequest<ServiceResult<List<EmployeeDto>>>;
}
