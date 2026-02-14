using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.UnitGroup;

public record AddUnitGroupCommand(string Name, string? Code, int PropertyId) : IRequest<BaseResponse<UnitGroupDTO>>;

public sealed class AddUnitGroupCommandHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddUnitGroupCommand, BaseResponse<UnitGroupDTO>>
{
    public async Task<BaseResponse<UnitGroupDTO>> Handle(AddUnitGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.TMS.UnitGroup
        {
            Name = request.Name,
            Code = request.Code,
            PropertyId = request.PropertyId
        };
        await repository.AddAsync(entity);
        return Success(new UnitGroupDTO { Id = entity.Id, Name = entity.Name, Code = entity.Code, PropertyId = entity.PropertyId });
    }
}
