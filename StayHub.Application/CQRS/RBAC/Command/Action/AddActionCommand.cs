using MediatR;
using Shared.Common;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Action
{
    // Include properties to be used as input for the command
    public record AddActionCommand(string Path, HttpVerb Method, bool AllowAnon) : IRequest<ActionDTO>;
  public class AddActionCommandHandler : IRequestHandler<AddActionCommand,ActionDTO >
    {
        private readonly IActionRepository _actionRepository;
        public AddActionCommandHandler(IActionRepository repository)
        {
            _actionRepository = repository;

        }
        public async Task<ActionDTO> Handle(AddActionCommand request, CancellationToken cancellationToken)
        {
            Domain.Entity.RBAC.Action action= new Domain.Entity.RBAC.Action 
            {
                     Path = request.Path,
                    AllowAnonymous = request.AllowAnon,
                    Method = request.Method
           
            };
            await _actionRepository.AddAsync(action);
            return new ActionDTO
            {
                Path = request.Path,
                AllowAnonymous = request.AllowAnon,
                Id = action.Id,
                CreatedAt = action.CreatedAt,
                UpdatedAt = action.UpdatedAt,
                Method = request.Method
            };
            
        }
    }

}