using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.CRM;
using StayHub.Infrastructure.Persistence.EntityConfiguration.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Persistence.ModelBuilderDI
{
    public static class CRMConfigurationDI
    {
        public static ModelBuilder ApplyCRMConfiguration(this ModelBuilder modelbuilder)
        {

            modelbuilder.ApplyConfiguration(new CustomerConfiguration());
            modelbuilder.ApplyConfiguration(new VehicleConfiguration());
            modelbuilder.ApplyConfiguration(new ContractServiceConfiguration());
            modelbuilder.ApplyConfiguration(new ContractAssetConfiguration());
            modelbuilder.ApplyConfiguration(new ContractConfiguration());
            return modelbuilder;
        }
    }
}
