using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.Catalog.Command.Province;
using StayHub.Application.CQRS.Catalog.Command.Ward;
using StayHub.Application.CQRS.Catalog.Query.Province;
using StayHub.Application.CQRS.Catalog.Query.Ward;
using StayHub.Application.CQRS.TMS.Command.Property;
using StayHub.Application.CQRS.TMS.Query.Property;

namespace StayHub.API.Controllers.TMS;

public class AddressController : BaseController
{
    [HttpPost("province")]
    public async Task<IActionResult> CreateProvince(AddProvinceCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpGet("province")]
    public async Task<IActionResult> GetAllProvince() => Ok(await Mediator.Send(new GetAllProvinceQuery()));

    [HttpGet("province/{id}")]
    public async Task<IActionResult> GetProvinceById(int id) => Ok(await Mediator.Send(new GetProvinceByIdQuery(id)));

    [HttpPut("province/{id}")]
    public async Task<IActionResult> UpdateProvince(int id, UpdateProvinceCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("province/{id}")]
    public async Task<IActionResult> DeleteProvince(int id) => GenerateResponse(await Mediator.Send(new DeleteProvinceCommand(id)));
    
    [HttpPost("ward")]
    public async Task<IActionResult> CreateWard(AddWardCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpGet("ward")]
    public async Task<IActionResult> GetAll() => Ok(await Mediator.Send(new GetAllWardQuery()));

    [HttpGet("ward/{id}")]
    public async Task<IActionResult> GetWardById(int id) => Ok(await Mediator.Send(new GetWardByIdQuery(id)));

    [HttpPut("ward/{id}")]
    public async Task<IActionResult> UpdateWard(int id, UpdateWardCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("ward/{id}")]
    public async Task<IActionResult> DeleteWard(int id) => GenerateResponse(await Mediator.Send(new DeleteWardCommand(id)));
}
