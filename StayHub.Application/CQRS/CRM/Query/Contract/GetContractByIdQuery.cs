using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.PMM;
namespace StayHub.Application.CQRS.CRM.Query.Contract;

public record GetContractByIdQuery(int Id) : IRequest<BaseResponse<ContractDTO>>;
public sealed class GetContractByIdQueryHandler(IContractRepository repository) : BaseResponseHandler, IRequestHandler<GetContractByIdQuery, BaseResponse<ContractDTO>>
{
    public async Task<BaseResponse<ContractDTO>> Handle(GetContractByIdQuery request, CancellationToken ct)
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, include: e => e.Include(j => j.Unit).Include(j => j.Customers).Include(j=>j.Services), selector: (x) => new ContractDTO
        {
            Id = x.Id,
            UnitId = x.UnitId,
            Unit = new DTO.PMM.UnitDTO
            {
                Id = x.Unit.Id,
                Name = x.Unit.Name,
            },
            Customer = x.Customers.Select(e => new CustomerDTO
            {
                Id = e.Id,
                Name = e.Name,
                IsRepresentative = e.IsRepresentative,
            }).ToList(),
            Status = x.Status.ToString(),
            Price = x.Price,
            Deposit = x.Deposit,
            Assets = x.ContractAssets == null ? null : x.ContractAssets.Select(e => new ContractAssetDTO
            {
                AssetId = e.AssetId,
                Quantity = e.Quantity,
            }).ToList(),
            Services = x.Services == null ? null : x.Services.Select(e => new  ServiceDTO
            {
                Id=e.Id,
                Name  =e.Name   
            }).ToList(),
            DepositRemain = x.DepositRemain,
            VehicleNumber =x.VehicleNumber,
            DepositRemainEndDate = x.DepositRemainEndDate,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            PaymentPeriodId = x.PaymentPeriodId,
            Note = x.Note,
            Attachment = x.Attachment,
            Code = x.Code,
            IsSigned = x.IsSigned,
            SaleId = x.SaleId
        });
        return result == null ? Failure<ContractDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}