using Microsoft.Extensions.DependencyInjection;
using StayHub.Application;
using StayHub.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub
{
    public static class DependencyInjection
    {
    public static IServiceCollection AddAppDI(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddInfrastructureDI(configuration).AddApplicationDI(configuration);
            return service;
        }
    }
}
