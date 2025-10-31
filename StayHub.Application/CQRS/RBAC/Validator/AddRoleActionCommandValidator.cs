using FluentValidation;
using StayHub.Application.CQRS.RBAC.Command.RoleAction;

namespace StayHub.Application.CQRS.RBAC.Validator
{
    public class AddRoleActionCommandValidator : AbstractValidator<AddMenuActionCommand>
    {
        public AddRoleActionCommandValidator()
        {
            // Add validation rules here
        }
    }
}
