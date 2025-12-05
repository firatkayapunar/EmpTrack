using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Commands
{
    public record CreateEmployeeCommand(string RegistrationNumber, string FirstName, string LastName, int DepartmentId, int TitleId, DateTime StartDate) : IRequest<ServiceResult<EmployeeDto>>;
}
