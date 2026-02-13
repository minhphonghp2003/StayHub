using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.Catalog.Command.Ward;
using StayHub.Application.CQRS.Catalog.Query.Ward;

namespace StayHub.API.Controllers.Catalog;

public class WardController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddWardCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await Mediator.Send(new GetAllWardQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetWardByIdQuery(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateWardCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteWardCommand(id)));
}
