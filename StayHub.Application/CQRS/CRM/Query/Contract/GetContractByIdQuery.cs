using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using Microsoft.EntityFrameworkCore;
namespace StayHub.Application.CQRS.CRM.Query.Contract;

public record GetContractByIdQuery(int Id) : IRequest<BaseResponse<ContractDTO>>;
public sealed class GetContractByIdQueryHandler(IContractRepository repository) : BaseResponseHandler, IRequestHandler<GetContractByIdQuery, BaseResponse<ContractDTO>>
{
    public async Task<BaseResponse<ContractDTO>> Handle(GetContractByIdQuery request, CancellationToken ct)
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, include: e => e.Include(j => j.Unit).Include(j => j.Customers), selector: (x) => new ContractDTO
        {
            Id = x.Id,
            UnitId = x.UnitId,
            Unit = new DTO.PMM.UnitDTO
            {
                Id = x.Unit.Id,
                Name = x.Unit.Name,
            },
            Customer = x.Customers.Where(e => e.IsRepresentative == true).Select(e => new CustomerDTO
            {
                Id = e.Id,
                Name = e.Name,
            }).First(),
            Status = x.Status.ToString(),
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
        });
        return result == null ? Failure<ContractDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}