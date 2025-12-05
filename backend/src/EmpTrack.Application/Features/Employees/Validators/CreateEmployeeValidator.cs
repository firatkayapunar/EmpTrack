using EmpTrack.Application.Features.Employees.Commands;
using EmpTrack.Application.Interfaces.Repositories;
using FluentValidation;

namespace EmpTrack.Application.Features.Employees.Validators
{
    public sealed class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, ITitleRepository titleRepository)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.");

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty()
                .WithMessage("Registration number is required.")
                .MustAsync(async (regNo, ct) => !await employeeRepository
                .ExistsAsync(e => e.RegistrationNumber == regNo))
                .WithMessage("Registration number already exists.");

            RuleFor(x => x.DepartmentId)
                .MustAsync(async (deptId, ct) => await departmentRepository
                .ExistsAsync(d => d.Id == deptId))
                .WithMessage("Department not found.");

            RuleFor(x => x.TitleId)
                .MustAsync(async (titleId, ct) => await titleRepository
                .ExistsAsync(t => t.Id == titleId))
                .WithMessage("Title not found.");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required.")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Start date cannot be in the future.");
        }
    }
}
