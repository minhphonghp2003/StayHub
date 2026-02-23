using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Query.Action;
using StayHub.Application.CQRS.RBAC.Query.Menu;
using StayHub.Application.CQRS.TMS.Command.Tier;
using StayHub.Application.CQRS.TMS.Query.Tier;

namespace StayHub.API.Controllers.TMS;

public class TierController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddTierCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await Mediator.Send(new GetAllTierQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetTierByIdQuery(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTierCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteTierCommand(id)));
   
    [HttpPost("assign-action")]
    public async Task<IActionResult> AssignRoleToAction(AssignActionsToTierCommand request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }
    [HttpGet("action-of-tier/{id}")]
    public async Task<IActionResult> GetActionOfRole(int id)
    {
        return GenerateResponse(await Mediator.Send(new GetAllActionOfTierQuery(id)
        ));
    }
    [HttpGet("menu-of-tier/{roleId}")]
    public async Task<IActionResult> GetMenuOfRole(int roleId)
    {
        return Ok(await Mediator.Send(new GetAllMenuOfTier(roleId)));
    }
    [HttpPost("assign-menu")]
    public async Task<IActionResult> AssignMenuToRole(AssignMenuToTierCommand request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }
    [HttpPost("renew-subscription")]
    public async Task<IActionResult> RenewSubscription(RenewSubscriptionCommand request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }
}
