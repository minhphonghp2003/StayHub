using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Ward;

public class UpdateWardCommand : IRequest<BaseResponse<WardDTO>>
{
    public int Id { get; set; }
}

public sealed class UpdateWardCommandHandler(IWardRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateWardCommand, BaseResponse<WardDTO>>
{
    public async Task<BaseResponse<WardDTO>> Handle(UpdateWardCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<WardDTO>("Not found", HttpStatusCode.BadRequest);
        
        repository.Update(entity);
        return Success(new WardDTO { Id = entity.Id });
    }
}
