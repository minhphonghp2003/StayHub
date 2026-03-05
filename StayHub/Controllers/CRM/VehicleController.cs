using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.CRM.Command.Vehicle;
using StayHub.Application.CQRS.CRM.Query.Vehicle;
namespace StayHub.API.Controllers.CRM;
public class VehicleController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetVehicleByIdQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllVehicleQuery(pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddVehicleCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateVehicleCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteVehicleCommand(id)));
}