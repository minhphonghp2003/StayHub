using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Job;
public class UpdateJobCommand : IRequest<BaseResponse<JobDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string? Name { get; set; }
}
public sealed class UpdateJobCommandHandler(IJobRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateJobCommand, BaseResponse<JobDTO>> 
{
    public async Task<BaseResponse<JobDTO>> Handle(UpdateJobCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<JobDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name ?? entity.Name;
        repository.Update(entity);
        return Success(new JobDTO { Id = entity.Id, Name = entity.Name });
    }
}