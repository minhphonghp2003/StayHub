using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Unit;

public class UpdateUnitCommand : IRequest<BaseResponse<UnitDTO>>
{
    public int Id { get; set; }
}

public sealed class UpdateUnitCommandHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateUnitCommand, BaseResponse<UnitDTO>>
{
    public async Task<BaseResponse<UnitDTO>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<UnitDTO>("Not found", HttpStatusCode.BadRequest);
        
        repository.Update(entity);
        return Success(new UnitDTO { Id = entity.Id });
    }
}
