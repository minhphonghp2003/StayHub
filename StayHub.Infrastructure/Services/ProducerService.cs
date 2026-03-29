using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
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
        private readonly IProducer<int, string> _producer;
        public ProducerService(IConfiguration configuration)
        {

            var config = new ProducerConfig { BootstrapServers = configuration.GetValue<string>("Kafka:BootstrapServers")};
            _producer = new ProducerBuilder<int, string>(config).Build();
        }
        public async Task SendEvent(string topic, Message<int,string> message)
        {
            await _producer.ProduceAsync(topic, message);
        }
    }
}
