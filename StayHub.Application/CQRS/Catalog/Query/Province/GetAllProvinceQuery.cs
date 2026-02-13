using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Province;

public record GetAllProvinceQuery() : IRequest<BaseResponse<List<ProvinceDTO>>>;

public sealed class GetAllProvinceQueryHandler(IProvinceRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAllProvinceQuery, BaseResponse<List<ProvinceDTO>>>
{
    public async Task<BaseResponse<List<ProvinceDTO>>> Handle(GetAllProvinceQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(selector: (e, i) => new ProvinceDTO { Id = e.Id });
        return Success(result.ToList());
    }
}
