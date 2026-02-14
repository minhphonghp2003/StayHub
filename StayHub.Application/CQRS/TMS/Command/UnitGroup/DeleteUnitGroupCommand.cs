using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.UnitGroup;

public record DeleteUnitGroupCommand(int Id) : IRequest<BaseResponse<bool>>;

public sealed class DeleteUnitGroupCommandHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteUnitGroupCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteUnitGroupCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(new Domain.Entity.TMS.UnitGroup { Id = request.Id });
        return Success(true);
    }
}
