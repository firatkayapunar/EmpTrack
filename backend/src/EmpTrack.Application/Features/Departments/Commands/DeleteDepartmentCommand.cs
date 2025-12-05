using EmpTrack.Application.Common.Results;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Commands
{
    public record DeleteDepartmentCommand(int Id) : IRequest<ServiceResult>;
}
