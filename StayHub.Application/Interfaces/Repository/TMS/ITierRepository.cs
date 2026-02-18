using StayHub.Domain.Entity.TMS;

namespace StayHub.Application.Interfaces.Repository.TMS;

public interface ITierRepository : IPagingAndSortingRepository<Tier>
{
    Task<bool> IsPropertyAlloweForActionAsync(int propertyId, string action);
    Task<bool> IsUnitAlloweForActionAsync(int unitId, string action);
}
