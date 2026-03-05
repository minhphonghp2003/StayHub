using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Contract;
public class UpdateContractCommand : IRequest<BaseResponse<ContractDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public int RoomId { get; set; }
    public long Price { get; set; }
    public long Deposit { get; set; }
    public long? DepositRemain { get; set; }
    public DateTime? DepositRemainEndDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PaymentPeriodId { get; set; }
    public string? Note { get; set; }
    public string? Attachment { get; set; }
    public string Code { get; set; }
    public bool IsSigned { get; set; }
    public int? TemplateId { get; set; }
}
public sealed class UpdateContractCommandHandler(IContractRepository repository) : BaseResponseHandler, IRequestHandler<UpdateContractCommand, BaseResponse<ContractDTO>> 
{
    public async Task<BaseResponse<ContractDTO>> Handle(UpdateContractCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<ContractDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.UnitId = request.RoomId;
        entity.Price = request.Price;
        entity.Deposit = request.Deposit;
        entity.DepositRemain = request.DepositRemain;
        entity.DepositRemainEndDate = request.DepositRemainEndDate;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.PaymentPeriodId = request.PaymentPeriodId;
        entity.Note = request.Note;
        entity.Attachment = request.Attachment;
        entity.Code = request.Code;
        entity.IsSigned = request.IsSigned;
        entity.TemplateId = request.TemplateId;

        repository.Update(entity);
        return Success(new ContractDTO 
        { 
            Id = entity.Id, 
            RoomId = entity.UnitId,
            Price = entity.Price,
            Deposit = entity.Deposit,
            DepositRemain = entity.DepositRemain,
            DepositRemainEndDate = entity.DepositRemainEndDate,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            PaymentPeriodId = entity.PaymentPeriodId,
            Note = entity.Note,
            Attachment = entity.Attachment,
            Code = entity.Code,
            IsSigned = entity.IsSigned,
            TemplateId = entity.TemplateId
        });
    }
}