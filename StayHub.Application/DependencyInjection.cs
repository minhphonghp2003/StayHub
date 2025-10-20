using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StayHub.Application.Middlewares;
using System.Reflection;

namespace StayHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection service, IConfiguration configuration)
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
