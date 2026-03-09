using Microsoft.AspNetCore.Mvc;
using Shared.Common;
using StayHub.Application.CQRS.CRM.Command.Contract;
using StayHub.Application.CQRS.CRM.Query.Contract;
namespace StayHub.API.Controllers.CRM;

public class ContractController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetContractByIdQuery(id)));

    [HttpGet("all/{propertyId}")]
    public async Task<IActionResult> GetAll(int propertyId,ContractStatus status, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search)
        => Ok(await Mediator.Send(new GetAllContractQuery(propertyId,status, pageNumber, pageSize, search)));

    [HttpPost("change-room/contract/{contractId}/unit/{unitId}")]
    public async Task<IActionResult> ChangeRoom(int contractId, int unitId) => GenerateResponse(await Mediator.Send(new ChangeRoomCommand(contractId, unitId)));
    [HttpPost("renew")]
    public async Task<IActionResult> Renew( RenewContractCommand command)
    {

        return GenerateResponse(await Mediator.Send(command));
    }
    [HttpPost("register-leaving")]
    public async Task<IActionResult> RequestLeave( RegisterLeavingCommand command)
    {

        return GenerateResponse(await Mediator.Send(command));
    }
    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer(TransferContractCommand command)
    {

        return GenerateResponse(await Mediator.Send(command));
    }
    [HttpPost]
    public async Task<IActionResult> Create(AddContractCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateContractCommand request)
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteContractCommand(id)));
}