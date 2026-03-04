using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.UnitGroup;
public class UpdateUnitGroupCommand : IRequest<BaseResponse<UnitGroupDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string? Name { get; set; }
}
public sealed class UpdateUnitGroupCommandHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateUnitGroupCommand, BaseResponse<UnitGroupDTO>> 
{
    public async Task<BaseResponse<UnitGroupDTO>> Handle(UpdateUnitGroupCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<UnitGroupDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name ?? entity.Name;
        repository.Update(entity);
        return Success(new UnitGroupDTO { Id = entity.Id, Name = entity.Name });
    }
}