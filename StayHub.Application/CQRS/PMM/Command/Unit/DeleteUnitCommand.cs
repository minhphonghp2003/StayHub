using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Unit;
public record DeleteUnitCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteUnitCommandHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteUnitCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteUnitCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.PMM.Unit { Id = request.Id });
        return Success(true);
    }
}