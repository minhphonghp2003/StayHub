using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.Property;

public record GetAllPropertyQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<PropertyDTO>>;

public sealed class GetAllPropertyQueryHandler(IPropertyRepository repository,IConfiguration configuration) 
    : BaseResponseHandler, IRequestHandler<GetAllPropertyQuery, Response<PropertyDTO>>
{
    public async Task<Response<PropertyDTO>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.pageSize ?? configuration.GetValue<int>("PageSize");
        var (result,count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: pageSize,
            filter: x => request.searchKey == null || x.Name.Contains(request.searchKey),
            selector: (x, i) => new  PropertyDTO
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Type = new CategoryItemDTO { Id = x.TypeId, Name = x.Type.Name },
                Image = x.Image,    
                StartSubscriptionDate = x.StartSubscriptionDate,    
                EndSubscriptionDate = x.EndSubscriptionDate,
                SubscriptionStatus = x.SubscriptionStatusId.HasValue ? new CategoryItemDTO { Id = x.SubscriptionStatusId.Value, Name = x.SubscriptionStatus.Name } : null,
                LastPaymentDate = x.LastPaymentDate,
                Tier = new CategoryItemDTO { Id = x.TierId, Name = x.Tier.Name },
                UpdatedAt = x.UpdatedAt
            },
            include:e=>e.Include(j=>j.Tier).Include(j=>j.Type).Include(j=>j.SubscriptionStatus),orderBy:e=>e.OrderByDescending(j=>j.UpdatedAt)
            
        );
        return SuccessPaginated(result, count, pageSize, request.pageNumber??1);
    }
}
