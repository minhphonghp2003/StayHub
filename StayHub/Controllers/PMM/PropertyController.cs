using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.PMM.Command.Property;
using StayHub.Application.CQRS.PMM.Query.Property;
using StayHub.Application.CQRS.TMS.Command.Property;
using StayHub.Application.CQRS.TMS.Query.Property;

namespace StayHub.API.Controllers.PMM;

public class PropertyController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddPropertyCommand request) => GenerateResponse(await Mediator.Send(request));
    [HttpPost("default-setting")]
    public async Task<IActionResult> DefaultSetting(SetDefaultSettingCommand request) => GenerateResponse(await Mediator.Send(request));
    [HttpGet("default-setting/{propertyId}")]
    public async Task<IActionResult> GetDefaultSetting(int propertyId) => Ok(await Mediator.Send(new GetDefaultSettingQuery(propertyId)));

    [HttpGet]
    public async Task<IActionResult> GetAll(string? search = null, int? pageNumber = null, int? pageSize = null) => Ok(await Mediator.Send(new GetAllPropertyQuery(pageNumber, pageSize, search)));
    [HttpGet("my")]
    public async Task<IActionResult> GetMy() => Ok(await Mediator.Send(new GetMyPropertyQuery()));

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
