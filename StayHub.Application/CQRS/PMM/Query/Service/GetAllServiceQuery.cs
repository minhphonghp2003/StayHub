using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using Microsoft.EntityFrameworkCore;
namespace StayHub.Application.CQRS.PMM.Query.Service;

public record GetAllServiceQuery(int propertyId, int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<ServiceDTO>>;
public sealed class GetAllServiceQueryHandler(IServiceRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllServiceQuery, Response<ServiceDTO>>
{
    public async Task<Response<ServiceDTO>> Handle(GetAllServiceQuery request, CancellationToken ct)
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => x.PropertyId == request.propertyId && request.searchKey == null || x.Name.ToLower().Contains(request.searchKey.ToLower()),
            include: x => x.Include(j => j.UnitType),
            selector: (x, i) => new ServiceDTO
            {
                Id = x.Id,
                Name = x.Name,
                PropertyId = x.PropertyId,
                UnitTypeId = x.UnitTypeId,
                UnitType = new DTO.Catalog.CategoryItemDTO
                {
                    Name = x.UnitType.Name,
                    Id = x.UnitTypeId
                },
                Price = x.Price,
                IsActive = x.IsActive,
                Description = x.Description
            }
        );
        return SuccessPaginated(result.ToList(), count,size, request.pageNumber ?? 1);
    }
}