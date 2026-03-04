using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.UnitGroup;
public record DeleteUnitGroupCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteUnitGroupCommandHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteUnitGroupCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteUnitGroupCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.PMM.UnitGroup { Id = request.Id });
        return Success(true);
    }
}