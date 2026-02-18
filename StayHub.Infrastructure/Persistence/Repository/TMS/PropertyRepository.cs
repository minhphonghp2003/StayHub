using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.TMS;
using StayHub.Domain.Entity.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Infrastructure.Persistence.Repository.TMS;

public class PropertyRepository(AppDbContext context) : PagingAndSortingRepository<Property>(context), IPropertyRepository
{
    public async Task<PropertyDTO?> GetPropertyByIdAsync(int id)
    {
        return await FindOneAsync(filter: e => e.Id == id,selector: (e) => new PropertyDTO
            
        {
            Id = e.Id,
            Name = e.Name,
            Address = e.Address,
            Image = e.Image,
            StartSubscriptionDate = e.StartSubscriptionDate ,
            EndSubscriptionDate = e.EndSubscriptionDate,
            LastPaymentDate = e.LastPaymentDate,
            Tier = new CategoryItemDTO
            {
                Name = e.Tier.Name
            },
            SubscriptionStatus = new CategoryItemDTO
            {
                Name = e.SubscriptionStatus.Name
            },
            Type = new CategoryItemDTO
            {
                Id = e.Type.Id,
                Name = e.Type.Name
            }
        },include:e=>e.Include(j=>j.Province).Include(j=>j.Ward).Include(j=>j.Type).Include(j=>j.SubscriptionStatus).Include(j=>j.Tier));
    }

    public Task<bool> IsUserInPropertyAsync(int userId, int propertyId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsSubscriptionActiveAsync(int propertyId)
    {
        throw new NotImplementedException();
    }
}
