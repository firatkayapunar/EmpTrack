using EmpTrack.Application.Features.Titles.Commands;
using FluentValidation;

namespace EmpTrack.Application.Features.Titles.Validators
{
    public class UpdateTitleValidator : AbstractValidator<UpdateTitleCommand>
    {
        public UpdateTitleValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Title name is required.")
            .MaximumLength(100)
            .WithMessage("Title name must not exceed 100 characters.");
        }
    }
}
