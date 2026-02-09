using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using Shared.Services;
using Mailtrap;
using Mailtrap.Emails.Requests;
using Mailtrap.Emails.Responses;
using Mailtrap;
using Mailtrap.Core.Exceptions;

namespace StayHub.Application.CQRS.Common.Command.Email
{
public record UpdateLoginActivityEvent() : INotification;
public sealed class UpdateLoginActivityHandler( ) : INotificationHandler<SendForgetPasswordEmailEvent >
{
    public async Task Handle(SendForgetPasswordEmailEvent request, CancellationToken cancellationToken)
    {
        
    }
}
}
