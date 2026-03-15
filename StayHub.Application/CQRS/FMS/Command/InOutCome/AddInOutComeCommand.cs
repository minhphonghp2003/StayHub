using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Command.InOutCome;
public record AddInOutComeCommand(long Amount,int propertyId, int PaymentMethodId, string Payer, string Description, int TypeId, DateTime Date, int? ContractId, bool IsRepeatMonthly, bool IsOutCome) : IRequest<BaseResponse<bool>>;
public sealed class AddInOutComeCommandHandler(IInOutComeRepository repository) : BaseResponseHandler, IRequestHandler<AddInOutComeCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddInOutComeCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.FMS.InOutCome 
        { 
            Amount = request.Amount,
            PaymentMethodId = request.PaymentMethodId,
            PropertyId = request.propertyId,
            Payer = request.Payer,
            Description = request.Description,
            TypeId = request.TypeId,
            Date = request.Date,
            ContractId = request.ContractId,
            IsRepeatMonthly = request.IsRepeatMonthly,
            IsOutCome = request.IsOutCome
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}