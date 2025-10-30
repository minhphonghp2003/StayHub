using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Action
{
    // Include properties to be used as input for the query
    public record GetAllActionQuery() : IRequest<BaseResponse<List<ActionDTO>>>;
    public class GetAllActionQueryHandler : BaseResponseHandler, IRequestHandler<GetAllActionQuery, BaseResponse<List<ActionDTO>>>
    {
        private readonly IActionRepository _actionRepository;
        public GetAllActionQueryHandler(IActionRepository actionRepo)
        {
            _actionRepository = actionRepo;

        }
        public async Task<BaseResponse<List<ActionDTO>>> Handle(GetAllActionQuery request, CancellationToken cancellationToken)
        {
            var actions = await _actionRepository.GetAllAsync((entity, index) => new ActionDTO
            {
                Id = entity.Id,
                Path = entity.Path,
                Method = entity.Method.ToString(),
                AllowAnonymous = entity.AllowAnonymous,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,

            });
            return Success(actions.ToList());
        }
    }

}
