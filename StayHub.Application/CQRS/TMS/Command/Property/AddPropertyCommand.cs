using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Property;

public record AddPropertyCommand() : IRequest<BaseResponse<PropertyDTO>>;

public sealed class AddPropertyCommandHandler(IPropertyRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddPropertyCommand, BaseResponse<PropertyDTO>>
{
    public async Task<BaseResponse<PropertyDTO>> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.TMS.Property { };
        await repository.AddAsync(entity);
        return Success(new PropertyDTO { Id = entity.Id });
    }
}
