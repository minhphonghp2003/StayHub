using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Property;

public record DeletePropertyCommand(int Id) : IRequest<BaseResponse<bool>>;

public sealed class DeletePropertyCommandHandler(IPropertyRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeletePropertyCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(new Domain.Entity.TMS.Property { Id = request.Id });
        return Success(true);
    }
}
