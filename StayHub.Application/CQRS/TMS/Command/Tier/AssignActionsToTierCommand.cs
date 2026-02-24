using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier
{
    // Include properties to be used as input for the command
    public record AssignActionsToTierCommand(List<int> actionIds, int tierId) : IRequest<Response<List<int>>>;
    public sealed class AssignActionsToTierCommandHandler(ITierRepository tierRepository,IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<AssignActionsToTierCommand, Response<List<int>>>
    {
        public async Task<Response<List<int>>> Handle(AssignActionsToTierCommand request, CancellationToken cancellationToken)
        {
           var tier = await tierRepository.FindOneEntityAsync(e=>e.Id==request.tierId,include:e=>e.Include(j=>j.Actions),trackChange:true);
            if(tier == null)
                return Failure<List<int>>("Tier not found.", System.Net.HttpStatusCode.NotFound);
            tier.Actions?.Clear();
            
            var actions = (await actionRepository.GetManyEntityAsync(e=>request.actionIds.Contains(e.Id))).ToList();
            foreach (var action in actions)
            {
                tier.Actions.Add(action);
            }
            await tierRepository.SaveAsync();
            return Success(request.actionIds, "Actions assigned to tier successfully.", System.Net.HttpStatusCode.OK);
        }
    }

}