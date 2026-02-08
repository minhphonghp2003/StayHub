using MediatR;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.Response;
using Shared.Services;

namespace StayHub.Application.CQRS.Common.Command.Email
{
public record SendForgetPasswordEmailCommand(string email,string token, int expiresMinute) : INotification;
public sealed class SendEmailCommandHandler(IConfiguration configuration ) : INotificationHandler<SendForgetPasswordEmailCommand >
{
    public async Task Handle(SendForgetPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        var fromAddress = configuration.GetValue<string>("SendGrid:FromEmail");
        var fromName = configuration.GetValue<string>("SendGrid:FromName");
        var subject = configuration.GetValue<string>("SendGrid:ResetPasswordSubject");
        var apiKey = configuration.GetValue<string>("SendGrid:ApiKey");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(fromAddress, fromName);
          var msg = new SendGridMessage
        {
            From = from,
            Subject = subject,
            TemplateId = "d-79520cc9d88b4da59950b2608481ee28"
        };
        msg.AddTo(new EmailAddress(request.email));

        var dynamicTemplateData = new {
            reset_password_url = "Google.com",
        };
        msg.SetTemplateData(dynamicTemplateData);
        var response = await client.SendEmailAsync(msg);
        Console.WriteLine(response);
    }
}
}
