using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Commands
{
    public record UpdateEmployeeCommand(int Id, string RegistrationNumber, string FirstName, string LastName, int DepartmentId, int TitleId, DateTime StartDate, bool IsActive) : IRequest<ServiceResult<EmployeeDto>>;
}
