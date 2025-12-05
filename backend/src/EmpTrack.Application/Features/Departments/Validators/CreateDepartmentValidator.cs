using EmpTrack.Application.Features.Departments.Commands;
using FluentValidation;

namespace EmpTrack.Application.Features.Departments.Validators
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(100)
            .WithMessage("Department name must not exceed 100 characters.");
        }
    }
}
