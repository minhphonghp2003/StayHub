using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.Catalog.Command.Province;
using StayHub.Application.CQRS.Catalog.Query.Province;

namespace StayHub.API.Controllers.Catalog;

public class ProvinceController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddProvinceCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await Mediator.Send(new GetAllProvinceQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetProvinceByIdQuery(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProvinceCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteProvinceCommand(id)));
}
