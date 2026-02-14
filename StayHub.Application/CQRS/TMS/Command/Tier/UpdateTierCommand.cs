using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier;


// public string Name { get; set; }
// public string? Description { get; set; }
// public string? Code { get; set; }
// public int Price { get; set; }
// public string BillingCycle { get; set; }
public class UpdateTierCommand : IRequest<BaseResponse<TierDTO>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public int Price { get; set; }
    public string BillingCycle { get; set; }

    public UpdateTierCommand()
    {
    }

    public UpdateTierCommand(string name, string? description, string? code, int price, string billingCycle)
    {
        Name = name;
        Description = description;
        Code = code;
        Price = price;
        BillingCycle = billingCycle;
        
    }
}

public sealed class UpdateTierCommandHandler(ITierRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateTierCommand, BaseResponse<TierDTO>>
{
    public async Task<BaseResponse<TierDTO>> Handle(UpdateTierCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<TierDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Code = request.Code;
        entity.Price = request.Price;
        entity.BillingCycle = request.BillingCycle;
        repository.Update(entity);
        return Success(new TierDTO { Id = entity.Id });
    }
}
