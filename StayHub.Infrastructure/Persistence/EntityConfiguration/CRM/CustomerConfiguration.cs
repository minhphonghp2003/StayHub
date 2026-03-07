using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Customer = StayHub.Domain.Entity.CRM.Customer;
namespace StayHub.Infrastructure.Persistence.EntityConfiguration.CRM;

public class CustomerConfiguration : BaseEntityConfiguration<Customer>
{
    override public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasIndex(p => p.Phone).IsUnique();
    }

}