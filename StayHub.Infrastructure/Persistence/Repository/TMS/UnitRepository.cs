using StayHub.Application.DTO.Catalog;
using StayHub.Domain.Entity.TMS;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;
using StayHub.Infrastructure.Persistence.Repository;

namespace StayHub.Infrastructure.Persistence.Repository.TMS;

public class UnitRepository(AppDbContext context) 
    : PagingAndSortingRepository<Unit>(context), IUnitRepository
{
    public async Task<(List<UnitDTO>, int)> GetAllUnitPaginated(int pageNumber, int pageSize, string? searchKey)
    {
        return await GetManyPagedAsync(
            filter: x => string.IsNullOrEmpty(searchKey) || x.Id.ToString().Contains(searchKey),
            selector: (x, i) => new UnitDTO { Id = x.Id,Name  =x.Name, PropertyId = x.UnitGroupId, BasePrice = x.BasePrice,Status = x.Status,  },
            pageNumber: pageNumber,
            pageSize: pageSize
        );
    }

    public async Task<List<UnitDTO>> GetAllUnitNoPaginated()
    {
        var result = await GetAllAsync((x, i) => new UnitDTO { Id = x.Id,Name  =x.Name, PropertyId = x.UnitGroupId, BasePrice = x.BasePrice,Status =x.Status 
       , });
        return result.ToList();
    }
}
