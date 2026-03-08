using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.CRM.Command.Contract
{
    public record TransferContractCommand(int contractId, int newCustomerId, DateTime transferDate) : IRequest<BaseResponse<bool>>;
    public sealed class TransferContractCommandHandler(IContractRepository contractRepository, ICustomerRepository customerRepository) : BaseResponseHandler, IRequestHandler<TransferContractCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(TransferContractCommand request, CancellationToken cancellationToken)
        {
            var contract = await contractRepository.GetEntityByIdAsync(request.contractId);
            if (contract == null)
            {
                return Failure<bool>("Không tìm thấy hợp đồng", System.Net.HttpStatusCode.BadRequest);
            }
            var customer = await customerRepository.GetEntityByIdAsync(request.newCustomerId);
            if (customer == null || customer.ContractId != null || customer.ContractId == request.contractId || customer.PropertyId != contract.Unit.UnitGroup.PropertyId)
            {

                return Failure<bool>("Không thể chuyển nhượng cho khách thuê này", System.Net.HttpStatusCode.BadRequest);
            }
            if (contract.StartDate > request.transferDate || contract.EndDate < request.transferDate)
            {

                return Failure<bool>("Không thể chuyển nhượng trong thời gian này", System.Net.HttpStatusCode.BadRequest);
            }
            contract.StartDate = request.transferDate;
            foreach(var c in contract.Customers)
            {
                if (c.IsRepresentative==true)
                {

                    c.IsRepresentative = false;
                }
            }
            contract.Customers = new List<Domain.Entity.CRM.Customer> { customer };
            await contractRepository.SaveAsync();
            return Success(true);
        }
    }
}
