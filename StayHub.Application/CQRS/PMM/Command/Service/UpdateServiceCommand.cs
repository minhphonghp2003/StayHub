using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Service;
public class UpdateServiceCommand : IRequest<BaseResponse<ServiceDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string Name { get; set; }
    public int UnitTypeId { get; set; }
    public long Price { get; set; }
    public int PropertyId { get; set; }
    public string? Description { get; set; }
}
public sealed class UpdateServiceCommandHandler(IServiceRepository repository) : BaseResponseHandler, IRequestHandler<UpdateServiceCommand, BaseResponse<ServiceDTO>> 
{
    public async Task<BaseResponse<ServiceDTO>> Handle(UpdateServiceCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<ServiceDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.UnitTypeId = request.UnitTypeId;

        repository.Update(entity);
        return Success(new ServiceDTO 
        { 
            Id = entity.Id, 
            Name = entity.Name,
            PropertyId = entity.PropertyId,
            IsActive = entity.IsActive,
            Description = entity.Description
        });
    }
}