using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Ward;

public record DeleteWardCommand(int Id) : IRequest<BaseResponse<bool>>;

public sealed class DeleteWardCommandHandler(IWardRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteWardCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteWardCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(new Domain.Entity.Catalog.Ward { Id = request.Id });
        return Success(true);
    }
}
