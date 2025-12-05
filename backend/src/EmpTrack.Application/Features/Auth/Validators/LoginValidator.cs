using EmpTrack.Application.Features.Auth.Dtos;
using FluentValidation;

namespace EmpTrack.Application.Features.Auth.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(4)
            .WithMessage("Password must be at least 4 characters.");
        }
    }
}
