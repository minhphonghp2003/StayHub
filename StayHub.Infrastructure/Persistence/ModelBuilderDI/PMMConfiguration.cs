using Microsoft.EntityFrameworkCore;
using StayHub.Infrastructure.Persistence.EntityConfiguration.PMM;
namespace StayHub.Infrastructure.Persistence.ModelBuilderDI
{
    public static class PMMConfigurationDI
    {
        public static ModelBuilder ApplyPMMConfiguration(this ModelBuilder modelbuilder)
        {

            modelbuilder.ApplyConfiguration(new PropertyConfiguration());
            modelbuilder.ApplyConfiguration(new UnitConfiguration());
            modelbuilder.ApplyConfiguration(new UnitGroupConfiguration());
            return modelbuilder;
        }
    }
}
