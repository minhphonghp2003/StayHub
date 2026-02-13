using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Property;

public class UpdatePropertyCommand : IRequest<BaseResponse<PropertyDTO>>
{
    public int Id { get; set; }
}

public sealed class UpdatePropertyCommandHandler(IPropertyRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdatePropertyCommand, BaseResponse<PropertyDTO>>
{
    public async Task<BaseResponse<PropertyDTO>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<PropertyDTO>("Not found", HttpStatusCode.BadRequest);
        
        repository.Update(entity);
        return Success(new PropertyDTO { Id = entity.Id });
    }
}
