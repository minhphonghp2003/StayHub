using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier;

public class UpdateTierCommand : IRequest<BaseResponse<TierDTO>>
{
    public int Id { get; set; }
}

public sealed class UpdateTierCommandHandler(ITierRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateTierCommand, BaseResponse<TierDTO>>
{
    public async Task<BaseResponse<TierDTO>> Handle(UpdateTierCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<TierDTO>("Not found", HttpStatusCode.BadRequest);
        
        repository.Update(entity);
        return Success(new TierDTO { Id = entity.Id });
    }
}
