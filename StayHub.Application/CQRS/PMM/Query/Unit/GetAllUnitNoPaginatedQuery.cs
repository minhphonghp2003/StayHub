using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.Unit;

public record GetAllUnitNoPaginatedQuery() : IRequest<BaseResponse<List<UnitDTO>>>;

public class GetAllUnitNoPaginatedQueryHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAllUnitNoPaginatedQuery, BaseResponse<List<UnitDTO>>>
{
    public async Task<BaseResponse<List<UnitDTO>>> Handle(GetAllUnitNoPaginatedQuery request, CancellationToken cancellationToken)
    {
        var data = await repository.GetAllUnitNoPaginated();
        return Success(data);
    }
}
