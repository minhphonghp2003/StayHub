using StayHub.Domain.Entity.TMS;
using StayHub.Application.Interfaces.Repository;
using StayHub.Application.DTO.TMS;

namespace StayHub.Application.Interfaces.Repository.TMS;

public interface IUnitGroupRepository : IPagingAndSortingRepository<UnitGroup>
{
    Task<(List<UnitGroupDTO>, int)> GetAllUnitGroupPaginated(int pageNumber, int pageSize, string? searchKey);
    Task<List<UnitGroupDTO>> GetAllUnitGroupNoPaginated();
    Task<UnitGroupDTO?> GetUnitGroupById(int id);
}
