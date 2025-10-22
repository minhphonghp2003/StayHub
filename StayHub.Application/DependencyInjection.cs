using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StayHub.Application.Interfaces.Services;
using StayHub.Application.Middlewares;
using StayHub.Application.Services;
using System.Reflection;

namespace StayHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            service.AddScoped<IJwtService, JwtService>();
            service.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return service;
        }
    }
}
