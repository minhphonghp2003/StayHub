using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.HRM.Command.Employee;
using StayHub.Application.CQRS.HRM.Query.Employee;

namespace StayHub.API.Controllers.HRM;

public class EmployeeController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddEmployeeCommand request) => GenerateResponse(await Mediator.Send(request));

   [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? searchKey)
    {
        return Ok(await Mediator.Send(new GetAllEmployeeQuery(pageNumber, pageSize, searchKey)));
    }

    [HttpGet("no-paginated")]
    public async Task<IActionResult> GetAllNoPaginated()
    {
        return Ok(await Mediator.Send(new GetAllEmployeeNoPaginatedQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetEmployeeByIdQuery(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteEmployeeCommand(id)));
}
