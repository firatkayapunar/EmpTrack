using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Departments.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Commands
{
    public record CreateDepartmentCommand(string Name, string? Description) : IRequest<ServiceResult<DepartmentDto>>;
}
