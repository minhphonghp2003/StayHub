using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.Catalog;
using StayHub.Infrastructure.Persistence.EntityConfiguration.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Persistence.ModelBuilderDI
{
    public static class CatalogConfigurationDI
    {
        public static ModelBuilder ApplyCatalogConfiguration(this ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new CategoryConfiguration());
            modelbuilder.ApplyConfiguration(new CategoryItemConfiguration());
            modelbuilder.ApplyConfiguration(new ProvinceConfiguration());
            return modelbuilder;
        }
    }
}
