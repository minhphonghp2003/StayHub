using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
using StayHub.Application.Interfaces.Repository.RBAC;
namespace StayHub.Application.CQRS.PMM.Command.Asset;
public record DeleteAssetCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteAssetCommandHandler(IAssetRepository repository,IHttpContextAccessor httpContextAccessor) : BaseResponseHandler, IRequestHandler<DeleteAssetCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteAssetCommand request, CancellationToken ct) 
    {
        await repository.DeleteWhere(e=>e.Id==request.Id && e.Property.Users.Any(u=>u.Id== httpContextAccessor.HttpContext.GetUserId()));
        return Success(true);
    }
}