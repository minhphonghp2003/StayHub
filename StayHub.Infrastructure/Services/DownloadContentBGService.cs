using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StayHub.Domain.Entity.Background;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Services
{
    public class DownloadContentBGService : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DownloadContentBGService> logger;
        public DownloadContentBGService(IConfiguration configuration, ILogger<DownloadContentBGService> logger)
        {
            this.logger = logger;
            var config = new ConsumerConfig
            {
                GroupId = "download-group",
                BootstrapServers = configuration.GetValue<string>("Kafka:BootstrapServers"),
                AutoOffsetReset = AutoOffsetReset.Earliest,

            };
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        private void StartConsumerLoop(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("download-content");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    var item = JsonConvert.DeserializeObject<DownloadedContent>(consumeResult.Message.Value);
                    logger.LogInformation(item.Url);

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing Kafka message.");
                }
            }
            _consumer.Close();
        }
        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Run(() => StartConsumerLoop(stoppingToken), stoppingToken);
            return Task.CompletedTask;
        }
    }
}
