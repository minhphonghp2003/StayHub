using File.Application;
using File.Infrastructure.Consumer;
using File.Infrastructure.Service;
using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using File.Infrastructure.Service.File;
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
            service.AddSingleton<IMinIOService, MinioService>();
            service.AddMinio(configureClient => configureClient
            .WithEndpoint(configuration["Minio:Endpoint"])
            .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
            .WithSSL(bool.Parse(configuration["Minio:Secure"]))
            .Build());
            // register Minio service implementation
            service.AddScoped<IMinIOService, MinioService>();
            service.AddMassTransit(x =>
            {
                              x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });

                x.AddRider(rider =>
                {
                    rider.AddConsumer<ExportFileConsumer>();
                    rider.AddProducer<int, FileExportedEvent>(FileExportedEvent.TopicName);

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(configuration["Kafka:BootstrapServers"]);

                        k.TopicEndpoint<ExportFileCommand>(ExportFileCommand.TopicName, "file-export-grp", e =>
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
