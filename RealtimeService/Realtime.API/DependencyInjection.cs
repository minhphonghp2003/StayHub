using Realtime.Application;
using Realtime.Infrastructure;

namespace Realtime.API
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
