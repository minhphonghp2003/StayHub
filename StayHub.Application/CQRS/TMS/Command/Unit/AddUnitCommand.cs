using MediatR;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Unit;
public record AddUnitCommand(int unitGroupId, UnitStatus status, decimal basePrice,string Name) : IRequest<BaseResponse<UnitDTO>>;

public sealed class AddUnitCommandHandler(IUnitRepository repository,ICategoryRepository categoryRepository) 
    : BaseResponseHandler, IRequestHandler<AddUnitCommand, BaseResponse<UnitDTO>>
{
    public async Task<BaseResponse<UnitDTO>> Handle(AddUnitCommand request, CancellationToken cancellationToken)
    {

       
        var entity = new Domain.Entity.TMS.Unit
        {
            UnitGroupId = request.unitGroupId,
            Status = request.status,
            BasePrice = request.basePrice,
            Name = request.Name
        };
        await repository.AddAsync(entity);
        return Success(new UnitDTO { Id = entity.Id });
    }
}
