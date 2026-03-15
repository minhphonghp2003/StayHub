using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.FMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.FMS.Command.Invoice
{
    public record ToggleApproveInvoiceCommand(int Id, bool isApprove) : IRequest<BaseResponse<bool>>;
    public sealed class ToggleApproveInvoiceCommandHandler(IInvoiceRepository repository) : BaseResponseHandler, IRequestHandler<ToggleApproveInvoiceCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(ToggleApproveInvoiceCommand request, CancellationToken ct)
        {

            var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
            if (entity == null)
            {
                return Failure<bool>("Invoice not found", System.Net.HttpStatusCode.BadRequest);

            }
            if (entity.Status == Shared.Common.InvoiceStatus.Expired)
            {

                return Failure<bool>("Invoice has expired", System.Net.HttpStatusCode.BadRequest);
            }
            if (entity.RemainAmount == 0)
            {
                return Failure<bool>("Cannot process", System.Net.HttpStatusCode.BadRequest);
            }
            entity.Status = request.isApprove ? Shared.Common.InvoiceStatus.Active : Shared.Common.InvoiceStatus.Pending;

            await repository.SaveAsync();
            return Success(true);
        }
    }
}
