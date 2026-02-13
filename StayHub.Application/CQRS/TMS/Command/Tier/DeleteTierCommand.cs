using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier;

public record DeleteTierCommand(int Id) : IRequest<BaseResponse<bool>>;

public sealed class DeleteTierCommandHandler(ITierRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteTierCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteTierCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(new Domain.Entity.TMS.Tier { Id = request.Id });
        return Success(true);
    }
}
