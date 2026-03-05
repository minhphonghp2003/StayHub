using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.CRM.Command.Contract;
using StayHub.Application.CQRS.CRM.Query.Contract;
namespace StayHub.API.Controllers.CRM;
public class ContractController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetContractByIdQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllContractQuery(pageNumber, pageSize, search)));

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