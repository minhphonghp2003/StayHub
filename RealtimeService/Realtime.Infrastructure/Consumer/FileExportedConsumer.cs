using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realtime.Infrastructure.Consumer
{
     public class FileExportedConsumer(ILogger<FileExportedEvent> logger) : IConsumer<FileExportedEvent>
    {
        public Task Consume(ConsumeContext<FileExportedEvent> context)
        {
            logger.LogInformation("Received File Exported Event: Id: {Id}, FileName: {FileName}, ExportedAt: {ExportedAt}",
               context.Message.Id, context.Message.FileName, context.Message.ExportedAt);
            return Task.CompletedTask;
        }
    }
}
