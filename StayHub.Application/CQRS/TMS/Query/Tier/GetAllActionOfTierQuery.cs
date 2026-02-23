using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.RBAC.Query.Action
{
    public record GetAllActionOfTierQuery(int TierId) : IRequest<BaseResponse<List<ActionDTO>>>;
    public sealed class GetAllActionOfTierQueryHandler(ITierRepository tierRepository) : BaseResponseHandler, IRequestHandler<GetAllActionOfTierQuery, BaseResponse<List<ActionDTO>>>
    {
        public async Task<BaseResponse<List<ActionDTO>>> Handle(GetAllActionOfTierQuery request, CancellationToken cancellationToken)
        {
            return Success(data: (await tierRepository.FindOneAsync(filter:e=> e.Id==request.TierId,selector:(e)=>e.Actions.Select(a=>new ActionDTO
            {
                Id = a.Id,
                Path = a.Path,
                Method = a.Method
            }),include:e=>e.Include(j=>j.Actions))).ToList());
        }
    }
}
