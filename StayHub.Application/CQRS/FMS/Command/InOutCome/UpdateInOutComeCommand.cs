using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.FMS;
using StayHub.Application.Interfaces.Repository.FMS;
namespace StayHub.Application.CQRS.FMS.Command.InOutCome;
public class UpdateInOutComeCommand : IRequest<BaseResponse<InOutComeDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public long Amount { get; set; }
    public int PaymentMethod { get; set; }
    public string Payer { get; set; }
    public string Description { get; set; }
    public int TypeId { get; set; }
    public DateTime Date { get; set; }
    public int? ContractId { get; set; }
    public bool IsRepeatMonthly { get; set; }
    public bool IsOutCome { get; set; }
}
public sealed class UpdateInOutComeCommandHandler(IInOutComeRepository repository) : BaseResponseHandler, IRequestHandler<UpdateInOutComeCommand, BaseResponse<InOutComeDTO>> 
{
    public async Task<BaseResponse<InOutComeDTO>> Handle(UpdateInOutComeCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<InOutComeDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.Amount = request.Amount;
        entity.PaymentMethod = request.PaymentMethod;
        entity.Payer = request.Payer;
        entity.Description = request.Description;
        entity.TypeId = request.TypeId;
        entity.Date = request.Date;
        entity.ContractId = request.ContractId;
        entity.IsRepeatMonthly = request.IsRepeatMonthly;
        entity.IsOutCome = request.IsOutCome;

        repository.Update(entity);
        return Success(new InOutComeDTO 
        { 
            Id = entity.Id, 
            Amount = entity.Amount,
            PaymentMethod = entity.PaymentMethod,
            Payer = entity.Payer,
            Description = entity.Description,
            TypeId = entity.TypeId,
            Date = entity.Date,
            ContractId = entity.ContractId,
            IsRepeatMonthly = entity.IsRepeatMonthly,
            IsOutCome = entity.IsOutCome
        });
    }
}