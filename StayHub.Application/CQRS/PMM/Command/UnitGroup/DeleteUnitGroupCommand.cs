using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.UnitGroup;
public record DeleteUnitGroupCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteUnitGroupCommandHandler(IUnitGroupRepository repository,IHttpContextAccessor contextAccessor) 
    : BaseResponseHandler, IRequestHandler<DeleteUnitGroupCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteUnitGroupCommand request, CancellationToken ct) 
    {
        await repository.DeleteWhere(e=>e.Id==request.Id && e.Property.Users.Any(u=>u.Id==contextAccessor.HttpContext.GetUserId()));
        return Success(true);
    }
}