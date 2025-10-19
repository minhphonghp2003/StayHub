using MediatR;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Action
{
    // Include properties to be used as input for the query
    public record GetAllActionQuery() : IRequest<List<ActionDTO >>;
  public class GetAllActionQueryHandler : IRequestHandler<GetAllActionQuery, List<ActionDTO>>
    {
        private readonly IActionRepository _actionRepository;
        public GetAllActionQueryHandler(IActionRepository actionRepo)
        {
            _actionRepository = actionRepo;
            
        }
        public async Task<List<ActionDTO>> Handle(GetAllActionQuery request, CancellationToken cancellationToken)
        {
            var actions =  await _actionRepository.GetAllAsync((entity,index)=>new ActionDTO
            {
                Id = entity.Id,
                Path = entity.Path,
                Method = entity.Method,
                AllowAnonymous = entity.AllowAnonymous,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,

            });
            return actions.ToList();
        }
    }

}
