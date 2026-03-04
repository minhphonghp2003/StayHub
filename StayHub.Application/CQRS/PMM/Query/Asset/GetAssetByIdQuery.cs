using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.Asset;
public record GetAssetByIdQuery(int Id) : IRequest<BaseResponse<AssetDTO>>;
public sealed class GetAssetByIdQueryHandler(IAssetRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAssetByIdQuery, BaseResponse<AssetDTO>> 
{
    public async Task<BaseResponse<AssetDTO>> Handle(GetAssetByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new AssetDTO { Id = x.Id, Name = x.Name });
        return result == null ? Failure<AssetDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}