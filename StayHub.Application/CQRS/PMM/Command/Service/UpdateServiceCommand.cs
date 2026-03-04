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
    public string? Name { get; set; }
}
public sealed class UpdateServiceCommandHandler(IServiceRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateServiceCommand, BaseResponse<ServiceDTO>> 
{
    public async Task<BaseResponse<ServiceDTO>> Handle(UpdateServiceCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<ServiceDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name ?? entity.Name;
        repository.Update(entity);
        return Success(new ServiceDTO { Id = entity.Id, Name = entity.Name });
    }
}