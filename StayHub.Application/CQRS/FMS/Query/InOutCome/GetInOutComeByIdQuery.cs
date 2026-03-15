using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Query.InOutCome;
public record GetInOutComeByIdQuery(int Id) : IRequest<BaseResponse<InOutComeDTO>>;
public sealed class GetInOutComeByIdQueryHandler(IInOutComeRepository repository) : BaseResponseHandler, IRequestHandler<GetInOutComeByIdQuery, BaseResponse<InOutComeDTO>> 
{
    public async Task<BaseResponse<InOutComeDTO>> Handle(GetInOutComeByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new InOutComeDTO 
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
        });
        return result == null ? Failure<InOutComeDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}