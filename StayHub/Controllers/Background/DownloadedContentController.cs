using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.Background.Command.DownloadedContent;
using StayHub.Application.CQRS.Background.Query.DownloadedContent;
namespace StayHub.API.Controllers.Background;
public class DownloadedContentController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetDownloadedContentByIdQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllDownloadedContentQuery(pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddDownloadedContentCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDownloadedContentCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteDownloadedContentCommand(id)));
}