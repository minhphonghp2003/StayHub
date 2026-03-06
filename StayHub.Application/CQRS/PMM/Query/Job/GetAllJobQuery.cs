using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using Microsoft.EntityFrameworkCore;
namespace StayHub.Application.CQRS.PMM.Query.Job;
public record GetAllJobQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<JobDTO>>;
public sealed class GetAllJobQueryHandler(IJobRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllJobQuery, Response<JobDTO>> 
{
    public async Task<Response<JobDTO>> Handle(GetAllJobQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null || x.Name.Contains(request.searchKey),
            include:x=>x.Include(j=>j.Property  ).Include(j=>j.Unit),
            selector: (x, i) => new JobDTO 
            { 
                Id = x.Id, 
                Name = x.Name,
                Property = new PropertyDTO
                {
                    Id =x.Property.Id,
                    Name = x.Property.Name,
                },
                Unit = new UnitDTO
                {
                    Id=x.Unit.Id,
                    Name = x.Unit.Name,
                },
                Description = x.Description,
                IsActive = x.IsActive
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}