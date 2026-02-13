using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Unit;

public record AddUnitCommand() : IRequest<BaseResponse<UnitDTO>>;

public sealed class AddUnitCommandHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddUnitCommand, BaseResponse<UnitDTO>>
{
    public async Task<BaseResponse<UnitDTO>> Handle(AddUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.TMS.Unit { };
        await repository.AddAsync(entity);
        return Success(new UnitDTO { Id = entity.Id });
    }
}
