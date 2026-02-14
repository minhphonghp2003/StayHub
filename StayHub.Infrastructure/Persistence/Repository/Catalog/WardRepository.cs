using StayHub.Domain.Entity.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Infrastructure.Persistence.Repository.Catalog;

public class WardRepository(AppDbContext context) : PagingAndSortingRepository<Ward>(context), IWardRepository
{
}
