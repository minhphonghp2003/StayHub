using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.Catalog;
using StayHub.Infrastructure.Persistence.EntityConfiguration.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayHub.Infrastructure.Persistence.EntityConfiguration.TMS;

namespace StayHub.Infrastructure.Persistence.ModelBuilderDI
{
    public static class TMSConfigurationDI
    {
        public static ModelBuilder ApplyTMSConfiguration(this ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new PropertyConfiguration());
            modelbuilder.ApplyConfiguration(new TierConfiguration());
            return modelbuilder;
        }
    }
}
