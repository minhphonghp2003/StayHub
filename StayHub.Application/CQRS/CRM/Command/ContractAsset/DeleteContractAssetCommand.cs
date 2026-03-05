using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.ContractAsset;
public record DeleteContractAssetCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteContractAssetCommandHandler(IContractAssetRepository repository) : BaseResponseHandler, IRequestHandler<DeleteContractAssetCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteContractAssetCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.CRM.ContractAsset { Id = request.Id });
        return Success(true);
    }
}