using Confluent.Kafka;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Shared.Message;
using StayHub.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ITopicProducer<int,ExportFileCommand> _producer;

        public ProducerService(IConfiguration configuration, ITopicProducer<int,ExportFileCommand> producer)
        {

            _producer = producer;
        }
        public async Task SendExportFileCommand(int id, string name)
        {
            await _producer.Produce(id, new ExportFileCommand
            {
                Id = id,
                Name = name
            });
        }
    }
}
