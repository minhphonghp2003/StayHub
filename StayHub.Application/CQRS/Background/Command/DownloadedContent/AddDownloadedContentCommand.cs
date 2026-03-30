using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.Background;
using StayHub.Application.Services;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StayHub.Application.CQRS.Background.Command.DownloadedContent;

public record AddDownloadedContentCommand(string Name, string Url) : IRequest<BaseResponse<bool>>;
public class AddDownloadedContentCommandHandler(IDownloadedContentRepository repository, IProducerService service,IRedisCacheService redisCacheService, IConfiguration configuration) : BaseResponseHandler, IRequestHandler<AddDownloadedContentCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddDownloadedContentCommand request, CancellationToken ct)
    {
        var entity = new StayHub.Domain.Entity.Background.DownloadedContent
        {
            Name = request.Name,
            Url = request.Url
        };
        await repository.AddAsync(entity);
        await redisCacheService.SetAsync("downloadContent", entity);
        string jsonString = JsonSerializer.Serialize(entity);
        await service.SendEvent("download-content", new Message<int, string> { Key = entity.Id, Value = jsonString });
        return Success(true);
    }
}