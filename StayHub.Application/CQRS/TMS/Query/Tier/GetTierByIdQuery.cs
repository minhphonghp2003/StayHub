using System.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.Tier;

public record GetTierByIdQuery(int Id) : IRequest<BaseResponse<TierDTO>>;

internal sealed class GetTierByIdQueryHandler(ITierRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetTierByIdQuery, BaseResponse<TierDTO>>
{
    public async Task<BaseResponse<TierDTO>> Handle(GetTierByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.FindOneAsync(e => e.Id == request.Id, e => new TierDTO { Id = e.Id, Name = e.Name, Description = e.Description, Price = e.Price, BillingCycle = e.BillingCycle });
        return result == null ? Failure<TierDTO>("Not found",HttpStatusCode.BadRequest) : Success(result);
    }
}
