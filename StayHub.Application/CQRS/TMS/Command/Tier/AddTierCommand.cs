using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier;

public record AddTierCommand() : IRequest<BaseResponse<TierDTO>>;

public sealed class AddTierCommandHandler(ITierRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddTierCommand, BaseResponse<TierDTO>>
{
    public async Task<BaseResponse<TierDTO>> Handle(AddTierCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.TMS.Tier { };
        await repository.AddAsync(entity);
        return Success(new TierDTO { Id = entity.Id });
    }
}
