using FluentValidation;
using StayHub.Application.CQRS.RBAC.Command.Action;

namespace StayHub.Application.CQRS.RBAC.Validator
{
    public class AddActionCommandCommandValidator : AbstractValidator<AddActionCommand>
    {
        public AddActionCommandCommandValidator()
        {
            RuleFor(x => x.Path)
           .NotEmpty().WithMessage("Path is required.");
            RuleFor(x => x.Method)
                      .NotEmpty().WithMessage("Path is required.");
        }
    }
}
