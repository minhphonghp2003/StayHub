using StayHub.Domain.Entity.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Infrastructure.Persistence.Repository.Catalog;

public class ProvinceRepository(AppDbContext context) : Repository<Province>(context), IProvinceRepository
{
}
