using FluentValidation;
using StayHub.Application.CQRS.RBAC.Command.Menu;

namespace StayHub.Application.CQRS.RBAC.Validator
{
    public class AddMenuCommandValidator : AbstractValidator<AddMenuCommand>
    {
        public AddMenuCommandValidator()
        {
            // Add validation rules here
        }
    }
}
