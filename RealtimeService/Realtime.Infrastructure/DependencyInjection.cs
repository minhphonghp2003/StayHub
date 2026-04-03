using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Application;
using Realtime.Infrastructure.Consumer;
using Shared.Message;

namespace Realtime.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAppDI(configuration);
            service.AddMassTransit(x =>
            {
                const string fileExportedTopic = "file-exported";
                const string kafkaBrokerServers = "localhost:9092";
                x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });

                x.AddRider(rider =>
                {
                    rider.AddConsumer<FileExportedConsumer>();
                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(kafkaBrokerServers);

                        k.TopicEndpoint<FileExportedEvent>(fileExportedTopic, "file-export-realtime", e =>
                        {
                            e.ConfigureConsumer<FileExportedConsumer>(context);
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
