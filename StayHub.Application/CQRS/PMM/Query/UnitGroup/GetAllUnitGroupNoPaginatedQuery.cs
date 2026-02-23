using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.UnitGroup;

public record GetAllUnitGroupNoPaginatedQuery() : IRequest<BaseResponse<List<UnitGroupDTO>>>;

public class GetAllUnitGroupNoPaginatedQueryHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAllUnitGroupNoPaginatedQuery, BaseResponse<List<UnitGroupDTO>>>
{
    public async Task<BaseResponse<List<UnitGroupDTO>>> Handle(GetAllUnitGroupNoPaginatedQuery request, CancellationToken cancellationToken)
    {
        var data = await repository.GetAllUnitGroupNoPaginated();
        return Success(data);
    }
}
