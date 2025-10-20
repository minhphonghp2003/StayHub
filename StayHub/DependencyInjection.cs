using StayHub.Application;
using StayHub.Infrastructure;

namespace StayHub
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddApplicationDI(configuration).AddInfrastructureDI(configuration);
            return service;
        }
    }
}
