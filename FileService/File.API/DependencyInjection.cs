using File.Application;
using File.Infrastructure;
using MassTransit;

namespace File.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIDI(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddAppDI(configuration);
            service.AddInfraDI(configuration);
            return service;
        }
    }
}
