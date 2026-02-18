using StayHub.Domain.Entity.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Infrastructure.Persistence.Repository.TMS;

public class TierRepository(AppDbContext context) : PagingAndSortingRepository<Tier>(context), ITierRepository
{
    public Task<bool> IsPropertyAlloweForActionAsync(int propertyId, string action)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUnitAlloweForActionAsync(int unitId, string action)
    {
        throw new NotImplementedException();
    }
}
