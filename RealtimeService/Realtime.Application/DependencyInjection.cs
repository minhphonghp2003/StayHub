using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Realtime.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection service, IConfiguration configuration)
        {
            return service;
        }
    }
}
