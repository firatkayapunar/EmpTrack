using EmpTrack.Application.Features.Departments.Commands;
using FluentValidation;

namespace EmpTrack.Application.Features.Departments.Validators
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(100)
            .WithMessage("Department name must not exceed 100 characters.");
        }
    }
}
