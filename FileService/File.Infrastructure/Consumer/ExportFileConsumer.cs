using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Infrastructure.Consumer
{
    record ExportFileCommand
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
    public class ExportFileConsumer(ILogger<ExportFileConsumer> logger) : IConsumer<ExportFileCommand>
    {
        Task IConsumer<ExportFileCommand>.Consume(ConsumeContext<ExportFileCommand> context)
        {
            logger.LogInformation("Exporting file with Id: {Id} and Name: {Name}", context.Message.Id, context.Message.Name);

            return Task.CompletedTask;
        }
    }
}
