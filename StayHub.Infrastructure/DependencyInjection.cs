using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Infrastructure.Persistence;
using StayHub.Infrastructure.Persistence.Repository.RBAC;

namespace StayHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ITokenRepository, TokenRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();
            service.AddScoped<IMenuRepository, MenuRepository>();
            service.AddScoped<IActionRepository, ActionRepository>();
            service.AddScoped<ISigningKeyRepository, SigningKeyRepository>();
            return service;
        }
    }
}
