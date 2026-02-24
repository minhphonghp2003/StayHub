using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier
{
    // Include properties to be used as input for the command
    public record AssignMenuToTierCommand(List<int> menuIds, int tierId) : IRequest<Response<List<int>>>;

    public sealed class AssignMenuToTierCommandHandler(
        ITierRepository tierRepository,
        IActionRepository actionRepository)
        : BaseResponseHandler, IRequestHandler<AssignMenuToTierCommand, Response<List<int>>>
    {
        public async Task<Response<List<int>>> Handle(AssignMenuToTierCommand request,
            CancellationToken cancellationToken)
        {
            var actions =
                (await actionRepository.GetManyAsync(
                    filter: e => e.MenuActions.Any(ma => request.menuIds.Contains(ma.MenuId)), selector: (e, i) => e))
                .Distinct().ToList();
            var tier = await tierRepository.FindOneEntityAsync(e => e.Id == request.tierId,
                include: e => e.Include(j => j.Actions), trackChange: true);
            if (tier == null)
                return Failure<List<int>>("Tier not found.", System.Net.HttpStatusCode.NotFound);
            tier.Actions?.Clear();
            foreach (var action in actions)
            {
                tier.Actions.Add(action);
            }

            await tierRepository.SaveAsync();
            return Success(request.menuIds, "Menus assigned to tier successfully.");
        }
    }
}