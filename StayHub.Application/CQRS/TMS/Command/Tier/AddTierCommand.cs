using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier;

public record AddTierCommand(string Name, string? Description,string? Code,int Price,string BilingCycle  ) : IRequest<BaseResponse<TierDTO>>;

public sealed class AddTierCommandHandler(ITierRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddTierCommand, BaseResponse<TierDTO>>
{
    public async Task<BaseResponse<TierDTO>> Handle(AddTierCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.TMS.Tier
        {
            Name = request.Name,
            Description = request.Description,
            Code = request.Code,
            Price = request.Price,
            BillingCycle = request.BilingCycle
        };
        await repository.AddAsync(entity);
        return Success(new TierDTO { Id = entity.Id, Name = entity.Name, Description = entity.Description,  Price = entity.Price, BillingCycle = entity.BillingCycle });
    }
}
