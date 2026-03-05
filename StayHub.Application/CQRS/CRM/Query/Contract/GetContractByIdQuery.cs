using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.Contract;
public record GetContractByIdQuery(int Id) : IRequest<BaseResponse<ContractDTO>>;
public sealed class GetContractByIdQueryHandler(IContractRepository repository) : BaseResponseHandler, IRequestHandler<GetContractByIdQuery, BaseResponse<ContractDTO>> 
{
    public async Task<BaseResponse<ContractDTO>> Handle(GetContractByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new ContractDTO 
        { 
            Id = x.Id, 
            RoomId = x.UnitId,
            Price = x.Price,
            Deposit = x.Deposit,
            DepositRemain = x.DepositRemain,
            DepositRemainEndDate = x.DepositRemainEndDate,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            PaymentPeriodId = x.PaymentPeriodId,
            Note = x.Note,
            Attachment = x.Attachment,
            Code = x.Code,
            IsSigned = x.IsSigned,
            TemplateId = x.TemplateId
        });
        return result == null ? Failure<ContractDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}