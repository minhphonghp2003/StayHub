using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.UnitGroup;

public record AddUnitGroupCommand() : IRequest<BaseResponse<UnitGroupDTO>>;

public sealed class AddUnitGroupCommandHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddUnitGroupCommand, BaseResponse<UnitGroupDTO>>
{
    public async Task<BaseResponse<UnitGroupDTO>> Handle(AddUnitGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.TMS.UnitGroup { };
        await repository.AddAsync(entity);
        return Success(new UnitGroupDTO { Id = entity.Id });
    }
}
