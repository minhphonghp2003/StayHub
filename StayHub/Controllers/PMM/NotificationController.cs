using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.PMM.Command.Notification;
using StayHub.Application.CQRS.PMM.Query.Notification;
namespace StayHub.API.Controllers.PMM;
public class NotificationController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetNotificationByIdQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllNotificationQuery(pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddNotificationCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateNotificationCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteNotificationCommand(id)));
}