using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.Background;
using StayHub.Application.Interfaces.Repository.Background;
using StayHub.Application.Services;
namespace StayHub.Application.CQRS.Background.Query.DownloadedContent;

public record GetDownloadedContentByIdQuery(int Id) : IRequest<BaseResponse<DownloadedContentDTO>>;
public sealed class GetDownloadedContentByIdQueryHandler(IDownloadedContentRepository repository, IRedisCacheService redisCacheService) : BaseResponseHandler, IRequestHandler<GetDownloadedContentByIdQuery, BaseResponse<DownloadedContentDTO>>
{
    public async Task<BaseResponse<DownloadedContentDTO>> Handle(GetDownloadedContentByIdQuery request, CancellationToken ct)
    {
        var cached = await redisCacheService.GetAsync<StayHub.Domain.Entity.Background.DownloadedContent>("downloadContent");
        if (cached != null)
        {
            return Success(new DownloadedContentDTO
            {
                Id = cached.Id,
                Name = cached.Name,
                Url = cached.Url
            });
        }

        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new DownloadedContentDTO
        {
            Id = x.Id,
            Name = x.Name,
            Url = x.Url
        });
        return result == null ? Failure<DownloadedContentDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}