using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
using Microsoft.EntityFrameworkCore;
namespace StayHub.Application.CQRS.FMS.Query.InOutCome;

public record GetInOutComeByIdQuery(int Id) : IRequest<BaseResponse<InOutComeDTO>>;
public sealed class GetInOutComeByIdQueryHandler(IInOutComeRepository repository) : BaseResponseHandler, IRequestHandler<GetInOutComeByIdQuery, BaseResponse<InOutComeDTO>>
{
    public async Task<BaseResponse<InOutComeDTO>> Handle(GetInOutComeByIdQuery request, CancellationToken ct)
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, include: x => x.Include(j => j.PaymentMethod).Include(j => j.Type).Include(j => j.Contract).ThenInclude(j => j.Unit),
            selector: (x) => new InOutComeDTO
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
            });
        return result == null ? Failure<InOutComeDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}