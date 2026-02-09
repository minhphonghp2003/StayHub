using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.RBAC;

namespace StayHub.Infrastructure.Persistence.EntityConfigurationDI
{
    public static class RBACConfigurationDI
    {
        public static ModelBuilder ApplyRBACConfiguration(this ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new UserConfiguration());
            modelbuilder.ApplyConfiguration(new TokenConfiguration());
            modelbuilder.ApplyConfiguration(new RoleConfiguration());
            modelbuilder.ApplyConfiguration(new MenuConfiguration());
            modelbuilder.ApplyConfiguration(new ActionConfiguration());
            modelbuilder.ApplyConfiguration(new SigningKeyConiguration());
            modelbuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelbuilder.ApplyConfiguration(new RoleActionConfiguration());
            modelbuilder.ApplyConfiguration(new MenuActionConfiguration());
            modelbuilder.ApplyConfiguration(new ProfileConfiguration());
            modelbuilder.ApplyConfiguration(new LoginActivityConfiguration());
            return modelbuilder;
        }
    }
}
