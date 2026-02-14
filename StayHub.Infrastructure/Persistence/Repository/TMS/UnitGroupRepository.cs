using Microsoft.EntityFrameworkCore;
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
            selector: (x, i) => new UnitGroupDTO { Id = x.Id , Name = x.Name, Code = x.Code, PropertyId = x.PropertyId ,Units = x.Units.Select(u => new UnitDTO { Id = u.Id, BasePrice = u.BasePrice, Status = u.Status, PropertyId = u.UnitGroupId }).ToList() },
            pageNumber: pageNumber,
            pageSize: pageSize,
            include:    e=>e.Include(j=>j.Units)
        );
    }

    public async Task<List<UnitGroupDTO>> GetAllUnitGroupNoPaginated()
    {
        var result = await GetAllAsync((x, i) => new UnitGroupDTO { Id = x.Id, Name = x.Name, Code = x.Code, PropertyId = x.PropertyId });
        return result.ToList();
    }

    public async Task<UnitGroupDTO?> GetUnitGroupById(int id)
    {
       return await FindOneAsync(e => e.Id == id, e => new UnitGroupDTO { Id = e.Id, Name = e.Name, Code = e.Code, PropertyId = e.PropertyId ,Units = e.Units.Select(u=>new UnitDTO
       {
              Id = u.Id,
              BasePrice = u.BasePrice,
              Status = u.Status,
              Name = u.Name,
              PropertyId = u.UnitGroupId
              
          }).ToList()
       },include:e=>e.Include(j=>j.Units)); 
    }
}
