using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.Background;
using StayHub.Application.Interfaces.Repository.Background;
namespace StayHub.Application.CQRS.Background.Query.DownloadedContent;
public record GetAllDownloadedContentQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<DownloadedContentDTO>>;
public sealed class GetAllDownloadedContentQueryHandler(IDownloadedContentRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllDownloadedContentQuery, Response<DownloadedContentDTO>> 
{
    public async Task<Response<DownloadedContentDTO>> Handle(GetAllDownloadedContentQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => (request.searchKey == null || x.Name.ToLower().Contains(request.searchKey.ToLower())),
            selector: (x, i) => new DownloadedContentDTO 
            { 
                Id = x.Id, 
                Name = x.Name,
                Url = x.Url
            }
        );
        return SuccessPaginated(result.ToList(), count,size, request.pageNumber ?? 1);
    }
}