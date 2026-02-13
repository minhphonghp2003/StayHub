using StayHub.Domain.Entity.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Infrastructure.Persistence.Repository.Catalog;

public class WardRepository(AppDbContext context) : Repository<Ward>(context), IWardRepository
{
}
