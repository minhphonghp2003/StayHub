using StayHub.Application.DTO.TMS;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Application.Interfaces.Repository.TMS;

public interface IPropertyRepository : IPagingAndSortingRepository<Property>
{
    Task<PropertyDTO?> GetPropertyByIdAsync(int id);
  
    Task<(bool,bool,bool)> CheckTierAllowancesAsync(int userId, string method,string action, int? propertyId, int? unitId);
}
