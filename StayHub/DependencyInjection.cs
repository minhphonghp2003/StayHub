using StayHub.Application;
using StayHub.Infrastructure;

namespace StayHub
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddApplicationDI(configuration).AddInfrastructureDI(configuration);
            service.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000") // Allow all Origins
                            .AllowAnyHeader()  // Allow all headers (like Content-Type)
                            .AllowAnyMethod().AllowCredentials(); // Allow all HTTP methods (GET, POST, etc.)
                });
            });
            service.AddHttpContextAccessor();

            return service;
        }
    }
}
