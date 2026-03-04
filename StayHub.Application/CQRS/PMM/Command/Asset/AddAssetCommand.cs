using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Asset;
public record AddAssetCommand(string Name) : IRequest<BaseResponse<bool>>;
public sealed class AddAssetCommandHandler(IAssetRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddAssetCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddAssetCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.PMM.Asset { Name = request.Name };
        await repository.AddAsync(entity);
        return Success(true);
    }
}