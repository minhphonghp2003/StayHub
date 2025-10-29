using FluentValidation;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {
            // Add validation rules here
        }
    }
}
