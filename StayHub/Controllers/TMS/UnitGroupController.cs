using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.TMS.Command.UnitGroup;
using StayHub.Application.CQRS.TMS.Query.UnitGroup;

namespace StayHub.API.Controllers.TMS;

public class UnitGroupController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddUnitGroupCommand request) => GenerateResponse(await Mediator.Send(request));

   [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? searchKey)
    {
        return Ok(await Mediator.Send(new GetAllUnitGroupQuery(pageNumber, pageSize, searchKey)));
    }

    [HttpGet("no-paginated")]
    public async Task<IActionResult> GetAllNoPaginated()
    {
        return Ok(await Mediator.Send(new GetAllUnitGroupNoPaginatedQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetUnitGroupByIdQuery(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUnitGroupCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteUnitGroupCommand(id)));
}
