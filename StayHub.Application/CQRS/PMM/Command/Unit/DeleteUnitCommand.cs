using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Unit;

public record DeleteUnitCommand(int Id) : IRequest<BaseResponse<bool>>;

public sealed class DeleteUnitCommandHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteUnitCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(new Domain.Entity.TMS.Unit { Id = request.Id });
        return Success(true);
    }
}
