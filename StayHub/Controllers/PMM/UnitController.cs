using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.PMM.Command.Unit;
using StayHub.Application.CQRS.PMM.Query.Unit;
namespace StayHub.API.Controllers.PMM;
public class UnitController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetUnitByIdQuery(id)));

    [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetAll(int propertyId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllUnitQuery(propertyId, pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddUnitCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUnitCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteUnitCommand(id)));
}