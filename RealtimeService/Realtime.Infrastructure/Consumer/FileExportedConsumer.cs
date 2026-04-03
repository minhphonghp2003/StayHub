using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Realtime.Infrastructure.Hubs;
using Shared.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realtime.Infrastructure.Consumer
{
     public class FileExportedConsumer(ILogger<FileExportedEvent> logger , IHubContext<FileNotificationHub, IFileNotificationClient> hubContext) : IConsumer<FileExportedEvent>
    {
        public async Task Consume(ConsumeContext<FileExportedEvent> context)
        {
            logger.LogInformation("Received File Exported Event: Id: {Id}, FileName: {FileName}, ExportedAt: {ExportedAt}",
               context.Message.Id, context.Message.FileName, context.Message.ExportedAt);
            await hubContext.Clients.User("9").SendFileExportedNotification(context.Message.Id,context.Message.FileName );
        }
    }
}
