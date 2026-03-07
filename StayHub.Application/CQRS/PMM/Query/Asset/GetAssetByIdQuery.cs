using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using Microsoft.AspNetCore.Http;
using StayHub.Application.Extension;
namespace StayHub.Application.CQRS.PMM.Query.Asset;
public record GetAssetByIdQuery(int Id) : IRequest<BaseResponse<AssetDTO>>;
public sealed class GetAssetByIdQueryHandler(IAssetRepository repository,IHttpContextAccessor httpContextAccessor) : BaseResponseHandler, IRequestHandler<GetAssetByIdQuery, BaseResponse<AssetDTO>> 
{
    public async Task<BaseResponse<AssetDTO>> Handle(GetAssetByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id && x.Property.Users.Any(u=>u.Id==httpContextAccessor.HttpContext.GetUserId()), (x) => new AssetDTO 
        { 
            Id = x.Id, 
            Name = x.Name,
            Quantity = x.Quantity,
            Price = x.Price,
            Type = new DTO.Catalog.CategoryItemDTO
            {
                Name = x.Type.Name,
            },
            PropertyId = x.PropertyId,
            UnitId = x.UnitId,
            Note = x.Note,
            Image = x.Image
        });
        return result == null ? Failure<AssetDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}