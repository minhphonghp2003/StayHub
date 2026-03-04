using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Unit;
public class UpdateUnitCommand : IRequest<BaseResponse<UnitDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string? Name { get; set; }
}
public sealed class UpdateUnitCommandHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateUnitCommand, BaseResponse<UnitDTO>> 
{
    public async Task<BaseResponse<UnitDTO>> Handle(UpdateUnitCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<UnitDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name ?? entity.Name;
        repository.Update(entity);
        return Success(new UnitDTO { Id = entity.Id, Name = entity.Name });
    }
}