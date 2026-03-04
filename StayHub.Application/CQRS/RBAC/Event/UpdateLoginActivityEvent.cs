using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using Shared.Services;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.Common.Command.Email
{
public record UpdateLoginActivityEvent(LoginActivity activity) : INotification;
public sealed class UpdateLoginActivityHandler(ILoginActivityRepository loginActivityRepository ) : INotificationHandler<UpdateLoginActivityEvent >
{
    public async Task Handle(UpdateLoginActivityEvent request, CancellationToken cancellationToken)
    {
        await loginActivityRepository.AddAsync(request.activity);
        
    }
}
}
