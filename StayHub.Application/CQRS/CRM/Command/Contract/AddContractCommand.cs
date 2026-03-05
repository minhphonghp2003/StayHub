using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Contract;
public record AddContractCommand(int RoomId, long Price, long Deposit, long? DepositRemain, DateTime? DepositRemainEndDate, DateTime StartDate, DateTime EndDate, int PaymentPeriodId, string? Note, string? Attachment, string Code, bool IsSigned, int? TemplateId) : IRequest<BaseResponse<bool>>;
public sealed class AddContractCommandHandler(IContractRepository repository) : BaseResponseHandler, IRequestHandler<AddContractCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddContractCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.CRM.Contract 
        { 
            UnitId = request.RoomId,
            Price = request.Price,
            Deposit = request.Deposit,
            DepositRemain = request.DepositRemain,
            DepositRemainEndDate = request.DepositRemainEndDate,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PaymentPeriodId = request.PaymentPeriodId,
            Note = request.Note,
            Attachment = request.Attachment,
            Code = request.Code,
            IsSigned = request.IsSigned,
            TemplateId = request.TemplateId
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}