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
    public record RegisterLeavingCommand(int contractId, DateTime leaveDate) : IRequest<BaseResponse<bool>>;
    public sealed class RegisterLeavingCommandHandler(IContractRepository contractRepository) : BaseResponseHandler, IRequestHandler<RegisterLeavingCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(RegisterLeavingCommand request, CancellationToken cancellationToken)
        {
            var contract = await contractRepository.GetEntityByIdAsync(request.contractId);
            if (contract == null)
            {
                return Failure<bool>("Không tìm thấy hợp đồng", System.Net.HttpStatusCode.BadRequest);
            }

            contract.LeavingDate = (new List<DateTime> { request.leaveDate, contract.EndDate }).Min();
            await contractRepository.SaveAsync();
            return Success(true);
        }
    }
}
