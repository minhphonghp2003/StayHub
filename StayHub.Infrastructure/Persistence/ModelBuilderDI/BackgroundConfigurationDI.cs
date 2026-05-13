using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.Background;
using StayHub.Infrastructure.Persistence.EntityConfiguration.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Persistence.ModelBuilderDI
{
    public static class BackgroundConfigurationDI 
    {
        public static ModelBuilder ApplyBackgroundConfiguration(this ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new DownloadedContentConfiguration());
            return modelbuilder;
        }
    }
}
