using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Asset;
public record AddAssetCommand(string Name, int Quantity, int? Price, int TypeId, int PropertyId, int? UnitId, string? Note, string Image) : IRequest<BaseResponse<bool>>;
public sealed class AddAssetCommandHandler(IAssetRepository repository) : BaseResponseHandler, IRequestHandler<AddAssetCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddAssetCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.PMM.Asset 
        { 
            Name = request.Name,
            Quantity = request.Quantity,
            Price = request.Price,
            TypeId = request.TypeId,
            PropertyId = request.PropertyId,
            UnitId = request.UnitId,
            Note = request.Note,
            Image = request.Image
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}