using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.Job;
public record GetJobByIdQuery(int Id) : IRequest<BaseResponse<JobDTO>>;
public sealed class GetJobByIdQueryHandler(IJobRepository repository) : BaseResponseHandler, IRequestHandler<GetJobByIdQuery, BaseResponse<JobDTO>> 
{
    public async Task<BaseResponse<JobDTO>> Handle(GetJobByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new JobDTO 
        { 
            Id = x.Id, 
            Name = x.Name,
            PropertyId = x.PropertyId,
            UnitId = x.UnitId,
            Description = x.Description,
            IsActive = x.IsActive
        });
        return result == null ? Failure<JobDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}