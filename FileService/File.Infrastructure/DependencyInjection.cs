using File.Application;
using File.Infrastructure.Consumer;
using File.Infrastructure.Service;
using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAppDI(configuration);
            service.AddScoped<ProducerService>();
            service.AddMassTransit(x =>
            {
                const string exportFileTopicName = "export-file";
                const string fileExportedTopic = "file-exported";
                const string kafkaBrokerServers = "localhost:9092";
                x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });

                x.AddRider(rider =>
                {
                    rider.AddConsumer<ExportFileConsumer>();
                    rider.AddProducer<FileExportedEvent>(fileExportedTopic);

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(kafkaBrokerServers);

                        k.TopicEndpoint<ExportFileCommand>(exportFileTopicName, "file-export-grp", e =>
                        {
                            e.ConfigureConsumer<ExportFileConsumer>(context);
                            e.CreateIfMissing();
                            e.AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
                        });
                    });
                });
            });
            return service;
        }
    }
}
