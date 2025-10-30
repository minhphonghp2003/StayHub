using FluentValidation;

namespace StayHub.Application.CQRS.RBAC.Command.Role
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            // Add validation rules here
        }
    }
}
