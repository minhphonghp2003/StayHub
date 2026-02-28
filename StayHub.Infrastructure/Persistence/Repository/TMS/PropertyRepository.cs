using Microsoft.EntityFrameworkCore;
using Shared.Common;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Infrastructure.Persistence.Repository.TMS;

public class PropertyRepository(AppDbContext context, IRoleRepository roleRepository)
    : PagingAndSortingRepository<Property>(context), IPropertyRepository
{
    public async Task<PropertyDTO?> GetPropertyByIdAsync(int id)
    {
        return await FindOneAsync(filter: e => e.Id == id, selector: (e) => new PropertyDTO

            {
                Id = e.Id,
                Name = e.Name,
                Address = e.Address,
                Image = e.Image,
                StartSubscriptionDate = e.StartSubscriptionDate,
                EndSubscriptionDate = e.EndSubscriptionDate,
                LastPaymentDate = e.LastPaymentDate,
                ProvinceId = e.ProvinceId,
                WardId = e.WardId,
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
            },
            include: e => e.Include(j => j.Province).Include(j => j.Ward).Include(j => j.Type)
                .Include(j => j.SubscriptionStatus).Include(j => j.Tier));
    }

   

    public async Task<(bool, bool, bool)> CheckTierAllowancesAsync(int userId, string method, string action,
        int? propertyId, int? unitId)
    {
        var roles = (await roleRepository.GetManyAsync(filter: e => e.UserRoles.Any(ug => ug.UserId == userId),
            selector: (e, i) => e.Code)).ToList();
        if (roles.Any(r => r == SystemRole.SUPER_ADMIN.ToString()) || (propertyId == null && unitId == null))
        {
            return (true, true, true);
        }

        var property = await FindOneAsync(
            filter: e => e.Users.Any(u => u.Id == userId) && ((propertyId.HasValue && e.Id == propertyId) ||
                                                              (unitId.HasValue && e.UnitGroups.Any(ug =>
                                                                  ug.Units.Any(u => u.Id == unitId)))),
            include: e => e.Include(p => p.Tier).ThenInclude(t => t.Actions), selector:
            (e) =>
                new
                {
                    SubscriptionEnd = e.EndSubscriptionDate,
                    TierActions = e.Tier != null
                        ? e.Tier.Actions.Select(a => new { Method = a.Method, Path = a.Path }).ToList()
                        : null
                });
        if (property == null)
        {
            return (false, false, false);
        }

        var isSubscriptionActive = property.SubscriptionEnd >= DateTime.UtcNow;
        var isActionAllowed = (property.TierActions ?? []).Any(a => a.Method == method && a.Path == action);
        return (true, isSubscriptionActive, isActionAllowed);
    }
}