using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Ward;

public record GetAllWardQuery(int provinceId) : IRequest<BaseResponse<List<WardDTO>>>;

public sealed class GetAllWardQueryHandler(IWardRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAllWardQuery, BaseResponse<List<WardDTO>>>
{
    public async Task<BaseResponse<List<WardDTO>>> Handle(GetAllWardQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetManyAsync( filter:e=>e.ProvinceId==request.provinceId, selector: (e, i) => new WardDTO { Id = e.Id, Name = e.Name, Code = e.Code,  });
        return Success(result.ToList());
    }
}
