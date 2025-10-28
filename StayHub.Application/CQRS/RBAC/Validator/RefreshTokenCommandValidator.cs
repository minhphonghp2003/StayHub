using FluentValidation;
using StayHub.Application.CQRS.RBAC.Command.Token;

namespace StayHub.Application.CQRS.RBAC.Validator
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            // Add validation rules here
        }
    }
}
