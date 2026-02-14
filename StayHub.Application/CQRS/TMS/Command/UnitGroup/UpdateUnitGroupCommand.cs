using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.UnitGroup;

public class UpdateUnitGroupCommand : IRequest<BaseResponse<UnitGroupDTO>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int PropertyId { get; set; } 
    public UpdateUnitGroupCommand(){}

    public UpdateUnitGroupCommand(
        string name,
        string code,
        int propertyId)
    {
        Name = name;
        Code = code;
        PropertyId = propertyId;
        {

        }
    }
}

public sealed class UpdateUnitGroupCommandHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateUnitGroupCommand, BaseResponse<UnitGroupDTO>>
{
    public async Task<BaseResponse<UnitGroupDTO>> Handle(UpdateUnitGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<UnitGroupDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name;
        entity.Code = request.Code;
        entity.PropertyId = request.PropertyId;
        repository.Update(entity);
        return Success(new UnitGroupDTO { Id = entity.Id });
    }
}
