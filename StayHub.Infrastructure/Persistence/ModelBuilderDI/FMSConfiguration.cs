using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.FMS;
using StayHub.Infrastructure.Persistence.EntityConfiguration.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Persistence.ModelBuilderDI
{
    public static class FMSConfiguration
    {
        public static ModelBuilder ApplyFMSConfiguration(this ModelBuilder modelbuilder)
        {

            modelbuilder.ApplyConfiguration(new InvoiceConfiguration());
            return modelbuilder;
        }
    }
}
