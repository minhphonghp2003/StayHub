using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.TMS.Command.Unit;
using StayHub.Application.CQRS.TMS.Query.Unit;

namespace StayHub.API.Controllers.TMS;

public class UnitController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddUnitCommand request) => GenerateResponse(await Mediator.Send(request));

   [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? searchKey)
    {
        return Ok(await Mediator.Send(new GetAllUnitQuery(pageNumber, pageSize, searchKey)));
    }

    [HttpGet("no-paginated")]
    public async Task<IActionResult> GetAllNoPaginated()
    {
        return Ok(await Mediator.Send(new GetAllUnitNoPaginatedQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetUnitByIdQuery(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUnitCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteUnitCommand(id)));
}
