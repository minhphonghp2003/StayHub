using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Ward;

public class UpdateWardCommand : IRequest<BaseResponse<WardDTO>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int? ProvinceId { get; set; }
}

public sealed class UpdateWardCommandHandler(IWardRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateWardCommand, BaseResponse<WardDTO>>
{
    public async Task<BaseResponse<WardDTO>> Handle(UpdateWardCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<WardDTO>("Not found", HttpStatusCode.BadRequest);
        if (await repository.ExistsAsync(e => e.Code == request.Code && e.Id != request.Id))
        {
            return Failure<WardDTO>(message: "Duplicate CODE", code: HttpStatusCode.BadRequest);
        }
        entity.Name = request.Name ?? entity.Name;
        entity.Code = request.Code ?? entity.Code;
        entity.ProvinceId = request.ProvinceId ?? entity.ProvinceId;    
        repository.Update(entity);
        return Success(new WardDTO { Id = entity.Id, Name = entity.Name, Code = entity.Code });
    }
}
