using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.RoleMenu
{
    // Include properties to be used as input for the command
    public record AssignMenuToRoleCommand(List<int> menuIds, int roleId) : IRequest<Response<List<int>>>;
    public sealed class AssignMenuToRoleCommandHandler(IRoleActionRepository roleActionRepository,IMenuActionRepository menuActionRepository,IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<AssignMenuToRoleCommand, Response<List<int>>>
    {
        public async Task<Response<List<int>>> Handle(AssignMenuToRoleCommand request, CancellationToken cancellationToken)
        {

            var actions = (await actionRepository.GetManyAsync(filter:e=>e.MenuActions.Any(ma=>request.menuIds.Contains(ma.MenuId)),selector:(e,i)=>e.Id)).Distinct().ToList();
            await roleActionRepository.DeleteWhere(e => e.RoleId == request.roleId, false);
            var result = await roleActionRepository.AddRangeAsync(actions.Select(aid => new StayHub.Domain.Entity.RBAC.RoleAction
            {
                ActionId = aid,
                RoleId = request.roleId
            }).ToList());
            if (result == null)
                return Failure<List<int>>("Failed to assign menu to role.", System.Net.HttpStatusCode.OK);

            return Success(request.menuIds, "Menus assigned to role successfully.", System.Net.HttpStatusCode.OK);
        }
    }
}
