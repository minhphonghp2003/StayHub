using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Asset;
public record DeleteAssetCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteAssetCommandHandler(IAssetRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteAssetCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteAssetCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.PMM.Asset { Id = request.Id });
        return Success(true);
    }
}