using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Ward;

public record GetAllWardQuery() : IRequest<BaseResponse<List<WardDTO>>>;

public sealed class GetAllWardQueryHandler(IWardRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAllWardQuery, BaseResponse<List<WardDTO>>>
{
    public async Task<BaseResponse<List<WardDTO>>> Handle(GetAllWardQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(selector: (e, i) => new WardDTO { Id = e.Id });
        return Success(result.ToList());
    }
}
