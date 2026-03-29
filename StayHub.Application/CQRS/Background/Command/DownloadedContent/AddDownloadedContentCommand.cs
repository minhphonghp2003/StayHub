using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.Background;
namespace StayHub.Application.CQRS.Background.Command.DownloadedContent;
public record AddDownloadedContentCommand(string Name, string Url) : IRequest<BaseResponse<bool>>;
public sealed class AddDownloadedContentCommandHandler(IDownloadedContentRepository repository) : BaseResponseHandler, IRequestHandler<AddDownloadedContentCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddDownloadedContentCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.Background.DownloadedContent 
        { 
            Name = request.Name,
            Url = request.Url
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}