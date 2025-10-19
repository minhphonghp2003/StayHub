using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application
{
    public static class DependencyInjection
    {
    public static IServiceCollection AddApplicationDI(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            service.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return service;
        }
    }
}
