using FluentValidation;
using StayHub.Application.CQRS.RBAC.Command.Action;

namespace StayHub.Application.CQRS.RBAC.Validator
{
    public class AddActionCommandCommandValidator : AbstractValidator<AddActionCommand>
    {
        public AddActionCommandCommandValidator()
        {
            // Add validation rules here
        }
    }
}
