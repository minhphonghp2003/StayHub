using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;
using StayHub.Application.CQRS.CRM.Command.Contract;
using StayHub.Application.CQRS.CRM.Query.Contract;
namespace StayHub.API.Controllers.CRM;

public class ContractController : BaseController
{

    [Authorize(Policy = "RequireContractAccess")]
    [HttpGet("{contractId}")]
    public async Task<IActionResult> GetById(int contractId) => Ok(await Mediator.Send(new GetContractByIdQuery(contractId)));

    [Authorize(Policy = "RequireContractAccess")]
    [HttpGet("all/{propertyId}")]
    public async Task<IActionResult> GetAll(int propertyId,ContractStatus? status, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search)
        => Ok(await Mediator.Send(new GetAllContractQuery(propertyId,status, pageNumber, pageSize, search)));

    [HttpPost("change-room/contract/{contractId}/unit/{unitId}")]
    public async Task<IActionResult> ChangeRoom(int contractId, int unitId) => GenerateResponse(await Mediator.Send(new ChangeRoomCommand(contractId, unitId)));

    [Authorize(Policy = "RequireContractAccess")]
    [HttpPost("renew")]
    public async Task<IActionResult> Renew( RenewContractCommand command)
    {

        return GenerateResponse(await Mediator.Send(command));
    }

    [Authorize(Policy = "RequireContractAccess")]
    [HttpPost("register-leaving")]
    public async Task<IActionResult> RequestLeave( RegisterLeavingCommand command)
    {

        return GenerateResponse(await Mediator.Send(command));
    }

    [Authorize(Policy = "RequireContractAccess")]
    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer(TransferContractCommand command)
    {

        return GenerateResponse(await Mediator.Send(command));
    }

    [Authorize(Policy = "RequireContractAccess")]
    [HttpPost]
    public async Task<IActionResult> Create(AddContractCommand request) => GenerateResponse(await Mediator.Send(request));

    [Authorize(Policy = "RequireContractAccess")]
    [HttpPut("{contractId}")]
    public async Task<IActionResult> Update(int contractId, UpdateContractCommand request)
    {
        request.Id = contractId;
        return GenerateResponse(await Mediator.Send(request));
    }

    [Authorize(Policy = "RequireContractAccess")]
    [HttpDelete("{contractId)}")]
    public async Task<IActionResult> Delete(int contractId) => GenerateResponse(await Mediator.Send(new DeleteContractCommand(contractId)));
}