using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.PMM.Command.Job;
using StayHub.Application.CQRS.PMM.Query.Job;
namespace StayHub.API.Controllers.PMM;
public class JobController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetJobByIdQuery(id)));

    [HttpGet("all/{propertyId}")]
    public async Task<IActionResult> GetAll(int propertyId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllJobQuery( propertyId, pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddJobCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateJobCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteJobCommand(id)));
}