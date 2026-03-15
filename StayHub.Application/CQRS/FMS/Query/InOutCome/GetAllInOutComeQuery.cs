using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Query.InOutCome;
public record GetAllInOutComeQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<InOutComeDTO>>;
public sealed class GetAllInOutComeQueryHandler(IInOutComeRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllInOutComeQuery, Response<InOutComeDTO>> 
{
    public async Task<Response<InOutComeDTO>> Handle(GetAllInOutComeQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => (request.searchKey == null || x.Name.ToLower().Contains(request.searchKey.ToLower())),
            selector: (x, i) => new InOutComeDTO 
            { 
                Id = x.Id, 
                Amount = x.Amount,
                PaymentMethod = x.PaymentMethod,
                Payer = x.Payer,
                Description = x.Description,
                TypeId = x.TypeId,
                Date = x.Date,
                ContractId = x.ContractId,
                IsRepeatMonthly = x.IsRepeatMonthly,
                IsOutCome = x.IsOutCome
            }
        );
        return SuccessPaginated(result.ToList(), count,size, request.pageNumber ?? 1);
    }
}