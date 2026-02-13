using System.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Province;

public record GetProvinceByIdQuery(int Id) : IRequest<BaseResponse<ProvinceDTO>>;

internal sealed class GetProvinceByIdQueryHandler(IProvinceRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetProvinceByIdQuery, BaseResponse<ProvinceDTO>>
{
    public async Task<BaseResponse<ProvinceDTO>> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.FindOneAsync(e => e.Id == request.Id, e => new ProvinceDTO { Id = e.Id });
        return result == null ? Failure<ProvinceDTO>("Not found",HttpStatusCode.BadRequest) : Success(result);
    }
}
