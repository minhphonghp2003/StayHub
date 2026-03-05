using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.Contract;
public record GetAllContractQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<ContractDTO>>;
public sealed class GetAllContractQueryHandler(IContractRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllContractQuery, Response<ContractDTO>> 
{
    public async Task<Response<ContractDTO>> Handle(GetAllContractQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null,
            selector: (x, i) => new ContractDTO 
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
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}