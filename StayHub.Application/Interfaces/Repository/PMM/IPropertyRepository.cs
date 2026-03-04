using StayHub.Application.DTO.PMM;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Application.Interfaces.Repository.PMM;

public interface IPropertyRepository : IPagingAndSortingRepository<Property>
{
    Task<PropertyDTO?> GetPropertyByIdAsync(int id);
  
    Task<(bool,bool,bool)> CheckTierAllowancesAsync(int userId, string method,string action, int? propertyId, int? unitId);
}
