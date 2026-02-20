using StayHub.Application.DTO.TMS;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Application.Interfaces.Repository.TMS;

public interface IPropertyRepository : IPagingAndSortingRepository<Property>
{
    Task<PropertyDTO?> GetPropertyByIdAsync(int id);
    Task<bool> IsUserInPropertyAsync(int userId, int propertyId);
    
    Task<bool> IsSubscriptionActiveAsync(int propertyId);
    Task<(bool,bool,bool)> CheckTierAllowancesAsync(int userId,List<string>? roles, string method,string action, int? propertyId, int? unitId);
}
