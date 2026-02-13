using System.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.Unit;

public record GetUnitByIdQuery(int Id) : IRequest<BaseResponse<UnitDTO>>;

internal sealed class GetUnitByIdQueryHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetUnitByIdQuery, BaseResponse<UnitDTO>>
{
    public async Task<BaseResponse<UnitDTO>> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.FindOneAsync(e => e.Id == request.Id, e => new UnitDTO { Id = e.Id });
        return result == null ? Failure<UnitDTO>("Not found",HttpStatusCode.BadRequest) : Success(result);
    }
}
