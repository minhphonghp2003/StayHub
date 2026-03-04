using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.Job;
public record GetAllJobQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<JobDTO>>;
public sealed class GetAllJobQueryHandler(IJobRepository repository, IConfiguration config) 
    : BaseResponseHandler, IRequestHandler<GetAllJobQuery, Response<JobDTO>> 
{
    public async Task<Response<JobDTO>> Handle(GetAllJobQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null || x.Name.Contains(request.searchKey),
            selector: (x, i) => new JobDTO { Id = x.Id, Name = x.Name }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}