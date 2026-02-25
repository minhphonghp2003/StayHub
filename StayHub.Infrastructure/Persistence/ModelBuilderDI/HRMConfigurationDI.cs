using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.Catalog;
using StayHub.Infrastructure.Persistence.EntityConfiguration.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayHub.Infrastructure.Persistence.EntityConfiguration.HRM;

namespace StayHub.Infrastructure.Persistence.ModelBuilderDI
{
    public static class HRMConfigurationDI
    {
        public static ModelBuilder ApplyHRMConfiguration(this ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new EmployeeConfiguration());
         
            return modelbuilder;
        }
    }
}
