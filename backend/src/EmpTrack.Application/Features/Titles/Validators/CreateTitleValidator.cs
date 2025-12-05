using EmpTrack.Application.Features.Titles.Commands;
using FluentValidation;

namespace EmpTrack.Application.Features.Titles.Validators
{
    public class CreateTitleValidator : AbstractValidator<CreateTitleCommand>
    {
        public CreateTitleValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Title name is required.")
            .MaximumLength(100)
            .WithMessage("Title name must not exceed 100 characters.");
        }
    }
}
