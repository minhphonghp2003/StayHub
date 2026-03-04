using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.TMS.Query.Property;

public record GetMyPropertyQuery() : IRequest<BaseResponse<List<PropertyDTO>>>;

internal sealed class GetMyPropertyQueryHandler(
    IPropertyRepository repository,
    IUserRepository userRepository,
    IHttpContextAccessor accessor)
    : BaseResponseHandler, IRequestHandler<GetMyPropertyQuery, BaseResponse<List<PropertyDTO>>>
{
    public async Task<BaseResponse<List<PropertyDTO>>> Handle(GetMyPropertyQuery request,
        CancellationToken cancellationToken)
    {
        var currentUserId = accessor.HttpContext.GetUserId();
        var isSystemUser = await userRepository.IsSystemUser(currentUserId ?? 0);
        var result = await repository.GetManyAsync(
            filter: e =>
                isSystemUser || (e.EndSubscriptionDate >= DateTime.UtcNow && e.Users.Any(u => u.Id == currentUserId)),
            selector: (e, i) => new PropertyDTO
            {
                Id = e.Id,
                Name = e.Name,
                Type = new CategoryItemDTO { Id = e.TypeId, Name = e.Type.Name },
                Image = e.Image,
                Address = e.Address,
                Tier = e.Tier != null ? new CategoryItemDTO { Id = e.Tier.Id, Name = e.Tier.Name } : null,
            }, include: e => e.Include(j => j.Tier).Include(j => j.Type).Include(j => j.SubscriptionStatus));
        return Success<List<PropertyDTO>>(result.ToList());
    }
}