using File.Application;
using File.Infrastructure.Consumer;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            service.AddMassTransit(x =>
            {
                x.UsingInMemory();

                x.AddRider(rider =>
                {
                    rider.AddConsumer<ExportFileConsumer>();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host("localhost:9092");

                        k.TopicEndpoint<ExportFileCommand>("export-file", "file-export-grp", e =>
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
