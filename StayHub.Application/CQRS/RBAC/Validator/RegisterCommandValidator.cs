using FluentValidation;
using StayHub.Application.CQRS.RBAC.Command.Token;

namespace StayHub.Application.CQRS.RBAC.Validator
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Password)
                      .NotEmpty().WithMessage("Password is required.");
        }
    }
}
