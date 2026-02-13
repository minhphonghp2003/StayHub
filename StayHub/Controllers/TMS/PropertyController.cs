using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.TMS.Command.Property;
using StayHub.Application.CQRS.TMS.Query.Property;

namespace StayHub.API.Controllers.TMS;

public class PropertyController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddPropertyCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await Mediator.Send(new GetAllPropertyQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetPropertyByIdQuery(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePropertyCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeletePropertyCommand(id)));
}
