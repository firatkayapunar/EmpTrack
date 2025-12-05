using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Departments.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Commands
{
    public record UpdateDepartmentCommand(int Id, string Name, string? Description) : IRequest<ServiceResult<DepartmentDto>>;
}
