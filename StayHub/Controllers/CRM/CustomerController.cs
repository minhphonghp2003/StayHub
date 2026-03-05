using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.CRM.Command.Customer;
using StayHub.Application.CQRS.CRM.Query.Customer;
namespace StayHub.API.Controllers.CRM;
public class CustomerController : BaseController 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetCustomerByIdQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search) 
        => Ok(await Mediator.Send(new GetAllCustomerQuery(pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddCustomerCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCustomerCommand request) 
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteCustomerCommand(id)));
}