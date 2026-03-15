using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
using Microsoft.EntityFrameworkCore;
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
            filter: x => (request.searchKey == null),
            include:x=>x.Include(j=>j.PaymentMethod).Include(j=>j.Type).Include(j=>j.Contract).ThenInclude(j=>j.Unit),
            selector: (x, i) => new InOutComeDTO
            {
                Id = x.Id,
                Amount = x.Amount,
                PaymentMethod = new DTO.Catalog.CategoryItemDTO
                {
                    Name = x.PaymentMethod.Name,
                },
                PaymentMethodId = x.PaymentMethodId,
                Payer = x.Payer,
                Description = x.Description,
                TypeId = x.TypeId,
                Type = new DTO.Catalog.CategoryItemDTO
                {
                    Name = x.Type.Name,
                },
                Date = x.Date,
                ContractId = x.ContractId,
                Contract = new DTO.CRM.ContractDTO
                {
                    Unit = new DTO.PMM.UnitDTO
                    {
                        Name = x.Contract.Unit.Name,
                    }
                },
                IsRepeatMonthly = x.IsRepeatMonthly,
                IsOutCome = x.IsOutCome
            }
        );
        return SuccessPaginated(result.ToList(), count, size, request.pageNumber ?? 1);
    }
}