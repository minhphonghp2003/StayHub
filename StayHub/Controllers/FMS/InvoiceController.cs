using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.FMS.Command.Invoice;
using StayHub.Application.CQRS.FMS.Query.Invoice;
namespace StayHub.API.Controllers.FMS;

public class InvoiceController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await Mediator.Send(new GetInvoiceByIdQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? search)
        => Ok(await Mediator.Send(new GetAllInvoiceQuery(pageNumber, pageSize, search)));

    [HttpPost]
    public async Task<IActionResult> Create(AddInvoiceCommand request) => GenerateResponse(await Mediator.Send(request));

    [HttpPost("toggle-approve/{id}")]
    public async Task<IActionResult> ToggleApprove(int id, bool isApprove) => GenerateResponse(await Mediator.Send(new ToggleApproveInvoiceCommand(id, isApprove)));
    [HttpPost("create-payment/{id}")]
    public async Task<IActionResult> CreatePayment(int id) => GenerateResponse(await Mediator.Send(new CreateInvoicePaymentCommand(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateInvoiceCommand request)
    {
        request.Id = id;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => GenerateResponse(await Mediator.Send(new DeleteInvoiceCommand(id)));
}