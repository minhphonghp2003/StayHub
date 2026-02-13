using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Province;

public record DeleteProvinceCommand(int Id) : IRequest<BaseResponse<bool>>;

public sealed class DeleteProvinceCommandHandler(IProvinceRepository repository) 
    : BaseResponseHandler, IRequestHandler<DeleteProvinceCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(new Domain.Entity.Catalog.Province { Id = request.Id });
        return Success(true);
    }
}
