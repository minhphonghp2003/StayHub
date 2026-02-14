using System.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.UnitGroup;

public record GetUnitGroupByIdQuery(int Id) : IRequest<BaseResponse<UnitGroupDTO>>;

internal sealed class GetUnitGroupByIdQueryHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetUnitGroupByIdQuery, BaseResponse<UnitGroupDTO>>
{
    public async Task<BaseResponse<UnitGroupDTO>> Handle(GetUnitGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.FindOneAsync(e => e.Id == request.Id, e => new UnitGroupDTO { Id = e.Id });
        return result == null ? Failure<UnitGroupDTO>("Not found",HttpStatusCode.BadRequest) : Success(result);
    }
}
