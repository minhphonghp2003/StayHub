using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return modelbuilder;
        }
    }
}
