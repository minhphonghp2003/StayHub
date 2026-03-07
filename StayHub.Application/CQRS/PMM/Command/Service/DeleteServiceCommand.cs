using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Service;
public record DeleteServiceCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteServiceCommandHandler(IServiceRepository repository,IHttpContextAccessor contextAccessor) : BaseResponseHandler, IRequestHandler<DeleteServiceCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteServiceCommand request, CancellationToken ct) 
    {
        await repository.DeleteWhere(e=>e.Id==request.Id && e.Property.Users.Any(u=>u.Id==contextAccessor.HttpContext.GetUserId()));
        return Success(true);
    }
}