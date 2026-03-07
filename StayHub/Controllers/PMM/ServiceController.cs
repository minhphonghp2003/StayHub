using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.PMM.Command.Service;
using StayHub.Application.CQRS.PMM.Query.Service;
namespace StayHub.API.Controllers.PMM;
public class ServiceController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetServiceByIdQuery(id)));

    [HttpGet("all/{propertyId}")]
    public async Task<IActionResult> GetAll( int propertyId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllServiceQuery(propertyId, pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddServiceCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateServiceCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteServiceCommand(id)));
}