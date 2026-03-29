using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.Background;
using StayHub.Application.Interfaces.Repository.Background;
namespace StayHub.Application.CQRS.Background.Query.DownloadedContent;
public record GetDownloadedContentByIdQuery(int Id) : IRequest<BaseResponse<DownloadedContentDTO>>;
public sealed class GetDownloadedContentByIdQueryHandler(IDownloadedContentRepository repository) : BaseResponseHandler, IRequestHandler<GetDownloadedContentByIdQuery, BaseResponse<DownloadedContentDTO>> 
{
    public async Task<BaseResponse<DownloadedContentDTO>> Handle(GetDownloadedContentByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new DownloadedContentDTO 
        { 
            Id = x.Id, 
            Name = x.Name,
            Url = x.Url
        });
        return result == null ? Failure<DownloadedContentDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}