using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.Background;
using StayHub.Application.Interfaces.Repository.Background;
namespace StayHub.Application.CQRS.Background.Command.DownloadedContent;
public class UpdateDownloadedContentCommand : IRequest<BaseResponse<DownloadedContentDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}
public sealed class UpdateDownloadedContentCommandHandler(IDownloadedContentRepository repository) : BaseResponseHandler, IRequestHandler<UpdateDownloadedContentCommand, BaseResponse<DownloadedContentDTO>> 
{
    public async Task<BaseResponse<DownloadedContentDTO>> Handle(UpdateDownloadedContentCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<DownloadedContentDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.Name = request.Name;
        entity.Url = request.Url;

        repository.Update(entity);
        return Success(new DownloadedContentDTO 
        { 
            Id = entity.Id, 
            Name = entity.Name,
            Url = entity.Url
        });
    }
}