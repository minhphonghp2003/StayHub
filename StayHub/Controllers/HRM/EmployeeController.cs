using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.HRM.Command.Employee;
using StayHub.Application.CQRS.HRM.Query.Employee;

namespace StayHub.API.Controllers.HRM;

public class EmployeeController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(AddEmployeeCommand request) => GenerateResponse(await Mediator.Send(request));

   [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetAll(int propertyId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? searchKey)
    {
        return Ok(await Mediator.Send(new GetAllEmployeeQuery(propertyId, pageNumber, pageSize, searchKey)));
    }

    [HttpDelete("{propertyId}/user/{id}")]
    public async Task<IActionResult> Delete(int id,int propertyId) => GenerateResponse(await Mediator.Send(new DeleteEmployeeCommand(id,propertyId)));
    
    [HttpPut("{propertyId}/user/{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeCommand request)
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }
}
