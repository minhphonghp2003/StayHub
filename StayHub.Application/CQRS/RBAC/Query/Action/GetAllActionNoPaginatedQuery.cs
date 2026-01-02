using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Action
{
    // Include properties to be used as input for the query
    public record GetAllActionNoPaginatedQuery() : IRequest<BaseResponse<List<ActionDTO>>>;
    public class GetAllActionNoPaginatedQueryHandler : BaseResponseHandler, IRequestHandler<GetAllActionNoPaginatedQuery, BaseResponse<List<ActionDTO>>>
    {
        private readonly IActionRepository _actionRepository;
        public GetAllActionNoPaginatedQueryHandler(IActionRepository actionRepo)
        {
            _actionRepository = actionRepo;

        }
        public async Task<BaseResponse<List<ActionDTO>>> Handle(GetAllActionNoPaginatedQuery request, CancellationToken cancellationToken)
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
