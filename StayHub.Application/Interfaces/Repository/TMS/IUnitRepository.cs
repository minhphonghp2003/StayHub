using StayHub.Domain.Entity.TMS;
using StayHub.Application.Interfaces.Repository;
using StayHub.Application.DTO.TMS;

namespace StayHub.Application.Interfaces.Repository.TMS;

public interface IUnitRepository : IPagingAndSortingRepository<Unit>
{
    Task<(List<UnitDTO>, int)> GetAllUnitPaginated(int pageNumber, int pageSize, string? searchKey);
    Task<List<UnitDTO>> GetAllUnitNoPaginated();
    Task<bool> IsUserInUnitAsync(int userId, int unitId);
    Task<bool> IsSubscriptionActiveAsync(int unitId);
}
