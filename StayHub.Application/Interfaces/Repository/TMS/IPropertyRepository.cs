using StayHub.Application.DTO.TMS;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Application.Interfaces.Repository.TMS;

public interface IPropertyRepository : IPagingAndSortingRepository<Property>
{
    Task<PropertyDTO?> GetPropertyByIdAsync(int id);
}
