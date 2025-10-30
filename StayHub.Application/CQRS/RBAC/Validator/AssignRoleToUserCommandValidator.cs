using FluentValidation;

namespace StayHub.Application.CQRS.RBAC.Command.UserRole
{
    public class AssignRoleToUserCommandValidator : AbstractValidator<AssignRoleToUserCommand>
    {
        public AssignRoleToUserCommandValidator()
        {
            // Add validation rules here
        }
    }
}
