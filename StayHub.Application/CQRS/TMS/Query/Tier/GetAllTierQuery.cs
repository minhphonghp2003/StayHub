using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.Tier;

public record GetAllTierQuery() : IRequest<BaseResponse<List<TierDTO>>>;

public sealed class GetAllTierQueryHandler(ITierRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAllTierQuery, BaseResponse<List<TierDTO>>>
{
    public async Task<BaseResponse<List<TierDTO>>> Handle(GetAllTierQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(selector: (e, i) => new TierDTO { Id = e.Id });
        return Success(result.ToList());
    }
}
