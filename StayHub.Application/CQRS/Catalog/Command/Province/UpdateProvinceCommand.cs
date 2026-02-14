using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Province;

public class UpdateProvinceCommand : IRequest<BaseResponse<ProvinceDTO>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public UpdateProvinceCommand() { }
    

    public UpdateProvinceCommand( string? name, string? code)
    {
        Name = name;
        Code = code;
    }
}

public sealed class UpdateProvinceCommandHandler(IProvinceRepository provinceRepository) 
    : BaseResponseHandler, IRequestHandler<UpdateProvinceCommand, BaseResponse<ProvinceDTO>>
{
    public async Task<BaseResponse<ProvinceDTO>> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
    {
        var entity = await provinceRepository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<ProvinceDTO>("Not found", HttpStatusCode.BadRequest);
        if (await provinceRepository.ExistsAsync(e => e.Code == request.Code && e.Id != request.Id))
        {
            return Failure<ProvinceDTO>(message: "Duplicate CODE", code: HttpStatusCode.BadRequest);
        } 
        entity.Name = request.Name ?? entity.Name;
        entity.Code = request.Code ?? entity.Code;  
        provinceRepository.Update(entity);
        return Success(new ProvinceDTO { Id = entity.Id, Name = entity.Name, Code = entity.Code });
    }
}
