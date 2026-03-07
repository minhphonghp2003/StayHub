using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Job;
public record DeleteJobCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteJobCommandHandler(IJobRepository repository,IHttpContextAccessor accessor) : BaseResponseHandler, IRequestHandler<DeleteJobCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteJobCommand request, CancellationToken ct) 
    {
        await repository.DeleteWhere(e=>e.Id==request.Id && e.Property.Users.Any(u=>u.Id==accessor.HttpContext.GetUserId()));
        return Success(true);
    }
}