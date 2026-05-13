using File.Infrastructure.Service;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Infrastructure.Consumer
{
    public class ExportFileConsumer : IConsumer<ExportFileCommand>
    {

        private readonly ILogger<FileExportedEvent> logger;
        private readonly ProducerService producerService;

        public ExportFileConsumer(ILogger<FileExportedEvent> logger, ProducerService producerService)
        {
            this.logger = logger;
            this.producerService = producerService;
        }
        async Task IConsumer<ExportFileCommand>.Consume(ConsumeContext<ExportFileCommand> context)
        {
            logger.LogInformation("Exporting file with Id: {Id} and Name: {Name}", context.Message.Id, context.Message.Name);
            await producerService.PublishExportedFileEvent(context.Message.UserId, context.Message.Id, context.Message.Name, DateTime.UtcNow);
        }
    }
}
