using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;

namespace StayHub.Application.CQRS.CRM.Command.Contract
{
    // Include properties to be used as input for the command
    public record RenewContractCommand(int contractId, DateTime newDate, int? newPrice, int? newDeposit) : IRequest<BaseResponse<bool>>;
    public sealed class RenewContractCommandHandler(IContractRepository contractRepository) : BaseResponseHandler, IRequestHandler<RenewContractCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(RenewContractCommand request, CancellationToken cancellationToken)
        {
            var contract = await contractRepository.GetEntityByIdAsync(request.contractId);
            if (contract == null)
            {
                return Failure<bool>("Không tìm thấy hợp đồng", System.Net.HttpStatusCode.BadRequest);
            }
            contract.EndDate = request.newDate;
            contract.Price = request.newPrice ?? contract.Price;
            contract.Deposit = request.newDeposit ?? contract.Deposit;
            await contractRepository.SaveAsync();
            return Success(true);
        }
    }

}