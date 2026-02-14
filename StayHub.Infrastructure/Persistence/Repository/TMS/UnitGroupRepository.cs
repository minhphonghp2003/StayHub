using StayHub.Domain.Entity.TMS;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;
using StayHub.Infrastructure.Persistence.Repository;

namespace StayHub.Infrastructure.Persistence.Repository.TMS;

public class UnitGroupRepository(AppDbContext context) 
    : PagingAndSortingRepository<UnitGroup>(context), IUnitGroupRepository
{
    public async Task<(List<UnitGroupDTO>, int)> GetAllUnitGroupPaginated(int pageNumber, int pageSize, string? searchKey)
    {
        return await GetManyPagedAsync(
            filter: x => string.IsNullOrEmpty(searchKey) || x.Id.ToString().Contains(searchKey),
            selector: (x, i) => new UnitGroupDTO { Id = x.Id },
            pageNumber: pageNumber,
            pageSize: pageSize
        );
    }

    public async Task<List<UnitGroupDTO>> GetAllUnitGroupNoPaginated()
    {
        var result = await GetAllAsync((x, i) => new UnitGroupDTO { Id = x.Id });
        return result.ToList();
    }
}
