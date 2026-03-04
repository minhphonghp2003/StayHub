using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.TMS;

namespace StayHub.Infrastructure.Persistence.ModelBuilderDI
{
    public static class TMSConfigurationDI
    {
        public static ModelBuilder ApplyTMSConfiguration(this ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new TierConfiguration());
            return modelbuilder;
        }
    }
}
