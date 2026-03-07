using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using StayHub.Application.Extension;
namespace StayHub.Application.CQRS.PMM.Query.Job;

public record GetJobByIdQuery(int Id) : IRequest<BaseResponse<JobDTO>>;
public sealed class GetJobByIdQueryHandler(IJobRepository repository,IHttpContextAccessor httpContextAccessor) : BaseResponseHandler, IRequestHandler<GetJobByIdQuery, BaseResponse<JobDTO>>
{
    public async Task<BaseResponse<JobDTO>> Handle(GetJobByIdQuery request, CancellationToken ct)
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id && x.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()), include: x => x.Include(j => j.Property).Include(j => j.Unit), selector: (x) => new JobDTO
        {
            Id = x.Id,
            Name = x.Name,
            Property = new PropertyDTO
            {
                Id = x.Property.Id,
                Name = x.Property.Name,
            },
           
            Description = x.Description,
            IsActive = x.IsActive
        });
        return result == null ? Failure<JobDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}