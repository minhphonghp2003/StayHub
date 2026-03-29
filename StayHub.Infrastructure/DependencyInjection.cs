using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StayHub.Application.Interfaces.Repository.Background;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Application.Interfaces.Repository.CRM;
using StayHub.Application.Interfaces.Repository.FMS;
using StayHub.Application.Interfaces.Repository.PMM;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Repository.TMS;
using StayHub.Application.Services;
using StayHub.Domain.Entity.PMM;
using StayHub.Infrastructure.Persistence;
using StayHub.Infrastructure.Persistence.Repository.Background;
using StayHub.Infrastructure.Persistence.Repository.Catalog;
using StayHub.Infrastructure.Persistence.Repository.CRM;
using StayHub.Infrastructure.Persistence.Repository.FMS;
using StayHub.Infrastructure.Persistence.Repository.PMM;
using StayHub.Infrastructure.Persistence.Repository.RBAC;
using StayHub.Infrastructure.Persistence.Repository.TMS;
using StayHub.Infrastructure.Security;
using StayHub.Infrastructure.Services;

namespace StayHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AppDbContext>(options =>
            options.UseLazyLoadingProxies()
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            service.AddSingleton<IProducerService, ProducerService>();
            service.AddHostedService<DownloadContentBGService>();
            service.AddScoped<IAuthorizationHandler,ContractAccessingHandler>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ITokenRepository, TokenRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();
            service.AddScoped<IMenuRepository, MenuRepository>();
            service.AddScoped<IActionRepository, ActionRepository>();
            service.AddScoped<ISigningKeyRepository, SigningKeyRepository>();
            service.AddScoped<IUserRoleRepository, UserRoleRepository>();
            service.AddScoped<IRoleActionRepository, RoleActionRepository>();
            service.AddScoped<IMenuActionRepository, MenuActionRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<ICategoryItemRepository, CategoryItemRepository>();
            service.AddScoped<ILoginActivityRepository, LoginActivityRepository>();
            service.AddScoped<IPropertyRepository, PropertyRepository>();
            service.AddScoped<ITierRepository, TierRepository>();
            service.AddScoped<IProvinceRepository, ProvinceRepository>();
            service.AddScoped<IWardRepository, WardRepository>();
            service.AddScoped<IUnitGroupRepository, UnitGroupRepository>();
            service.AddScoped<IUnitRepository, UnitRepository>();
            service.AddScoped<IAssetRepository, AssetRepository>();
            service.AddScoped<IJobRepository, JobRepository>();
            service.AddScoped<IServiceRepository, ServiceRepository>();
            service.AddScoped<ICustomerRepository, CustomerRepository>();
            service.AddScoped<INotificationRepository, NotificationRepository>();
            service.AddScoped<IContractRepository, ContractRepository>();
            service.AddScoped<IContractAssetRepository, ContractAssetRepository>();
            service.AddScoped<IVehicleRepository, VehicleRepository>();
            service.AddScoped<IInvoiceRepository, InvoiceRepository>();
            service.AddScoped<IInOutComeRepository, InOutComeRepository>();
            service.AddScoped<IDownloadedContentRepository, DownloadedContentRepository>();
            return service;
        }
    }
}
