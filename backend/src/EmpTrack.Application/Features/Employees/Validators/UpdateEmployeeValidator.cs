using EmpTrack.Application.Features.Employees.Commands;
using EmpTrack.Application.Interfaces.Repositories;
using FluentValidation;

namespace EmpTrack.Application.Features.Employees.Validators
{
    public sealed class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, ITitleRepository titleRepository)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty.");

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty()
                .MustAsync(async (cmd, regNo, ct) => !await employeeRepository
                    .ExistsAsync(e => e.RegistrationNumber == regNo && e.Id != cmd.Id))
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
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Start date cannot be in the future.");
        }
    }

}
