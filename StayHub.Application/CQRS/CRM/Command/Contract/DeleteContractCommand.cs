using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Contract;

public record DeleteContractCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteContractCommandHandler(IContractRepository repository, IHttpContextAccessor httpContextAccessor) : BaseResponseHandler, IRequestHandler<DeleteContractCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteContractCommand request, CancellationToken ct)
    {
        await repository.DeleteWhere(e => e.Unit.UnitGroup.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()));
        return Success(true);
    }
}