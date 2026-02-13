using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Ward;

public record AddWardCommand() : IRequest<BaseResponse<WardDTO>>;

public sealed class AddWardCommandHandler(IWardRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddWardCommand, BaseResponse<WardDTO>>
{
    public async Task<BaseResponse<WardDTO>> Handle(AddWardCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.Catalog.Ward { };
        await repository.AddAsync(entity);
        return Success(new WardDTO { Id = entity.Id });
    }
}
