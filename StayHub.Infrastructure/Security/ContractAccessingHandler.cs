using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.CRM;
using StayHub.Application.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Security
{
    public class ContractAccessingHandler(IHttpContextAccessor httpContextAccessor, IContractRepository contractRepository) : AuthorizationHandler<ContractAccessingRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ContractAccessingRequirement requirement)
        {
            var routeValue =(await httpContextAccessor.HttpContext?.GetStubFromRequest("contractId"))?.ToString();
            var userId = httpContextAccessor.HttpContext?.GetUserId();

            if (string.IsNullOrEmpty(routeValue) || !int.TryParse(routeValue, out int contractId))
            {
                context.Fail();
                return;
            }
            var canAccess = (await contractRepository.FindOneEntityAsync(e => e.Id == contractId && e.Unit.UnitGroup.Property.Users.Any(u => u.Id == userId))) != null;
            if (canAccess)
            {

                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }


        }
    }
}
