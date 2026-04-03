using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application;
using Notification.Infrastructure.Consumer;
using Shared.Message;

namespace Notification.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAppDI(configuration);
            service.AddMassTransit(x =>
            {
                            x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });

                x.AddRider(rider =>
                {
                    rider.AddConsumer<FileExportedConsumer>();
                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(configuration["Kafka:BootstrapServers"]);

                        k.TopicEndpoint<FileExportedEvent>(FileExportedEvent.TopicName, "file-export-noti", e =>
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
