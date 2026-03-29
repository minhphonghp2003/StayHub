using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.Background;
namespace StayHub.Application.CQRS.Background.Command.DownloadedContent;
public record DeleteDownloadedContentCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteDownloadedContentCommandHandler(IDownloadedContentRepository repository) : BaseResponseHandler, IRequestHandler<DeleteDownloadedContentCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteDownloadedContentCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.Background.DownloadedContent { Id = request.Id });
        return Success(true);
    }
}