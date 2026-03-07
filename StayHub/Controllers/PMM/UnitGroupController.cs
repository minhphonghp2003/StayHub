using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.PMM.Command.UnitGroup;
using StayHub.Application.CQRS.PMM.Query.UnitGroup;
namespace StayHub.API.Controllers.PMM;
public class UnitGroupController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetUnitGroupByIdQuery(id)));

    [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetAll(int propertyId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllUnitGroupQuery( propertyId,pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddUnitGroupCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUnitGroupCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteUnitGroupCommand(id)));
}