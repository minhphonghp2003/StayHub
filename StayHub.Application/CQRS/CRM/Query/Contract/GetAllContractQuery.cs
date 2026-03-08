using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using Microsoft.EntityFrameworkCore;
namespace StayHub.Application.CQRS.CRM.Query.Contract;

public record GetAllContractQuery(int propertyId, int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<ContractDTO>>;
public sealed class GetAllContractQueryHandler(IContractRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllContractQuery, Response<ContractDTO>>
{
    public async Task<Response<ContractDTO>> Handle(GetAllContractQuery request, CancellationToken ct)
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => x.Unit.UnitGroup.PropertyId==request.propertyId&& request.searchKey == null,
            include:e=>e.Include(j=>j.Unit).Include(j=>j.Customers),
            selector: (x, i) => new ContractDTO
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
            }
        );
        return SuccessPaginated(result.ToList(), count, size, request.pageNumber ?? 1);
    }
}