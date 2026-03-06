using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.Catalog;
namespace StayHub.Application.CQRS.PMM.Query.Asset;
public record GetAllAssetQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<AssetDTO>>;
public sealed class GetAllAssetQueryHandler(IAssetRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllAssetQuery, Response<AssetDTO>> 
{
    public async Task<Response<AssetDTO>> Handle(GetAllAssetQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null || x.Name.Contains(request.searchKey),
            include: x=>x.Include(e=>e.Type),
            selector: (x, i) => new AssetDTO 
            { 
                Id = x.Id, 
                Name = x.Name,
                Quantity = x.Quantity,
                Price = x.Price,
                Type =new CategoryItemDTO
                {
                    Name = x.Type.Name  ,

                },
                PropertyId = x.PropertyId,
                UnitId = x.UnitId,
                Note = x.Note,
                Image = x.Image
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}