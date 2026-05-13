using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Application;
using Realtime.Infrastructure.Consumer;
using Realtime.Infrastructure.Hubs;
using Shared.Message;

namespace Realtime.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAppDI(configuration);
            service.AddSignalR();
            service.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            service.AddMassTransit(x =>
            {
                x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });

                x.AddRider(rider =>
                {
                    rider.AddConsumer<FileExportedConsumer>();
                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(configuration["Kafka:BootstrapServers"]);

                        k.TopicEndpoint<FileExportedEvent>(FileExportedEvent.TopicName, "file-export-realtime", e =>
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
