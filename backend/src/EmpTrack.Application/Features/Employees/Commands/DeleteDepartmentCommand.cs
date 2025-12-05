using EmpTrack.Application.Common.Results;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Commands
{
    public record DeleteEmployeeCommand(int Id) : IRequest<ServiceResult>;
}
