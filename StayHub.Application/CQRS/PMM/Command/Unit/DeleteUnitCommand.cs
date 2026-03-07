using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Unit;

public record DeleteUnitCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteUnitCommandHandler(IUnitRepository repository, IHttpContextAccessor httpContextAccessor)
    : BaseResponseHandler, IRequestHandler<DeleteUnitCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteUnitCommand request, CancellationToken ct)
    {
        await repository.DeleteWhere(e => e.Id == request.Id && e.UnitGroup.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()));
        return Success(true);
    }
}