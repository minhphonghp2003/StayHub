using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Customer;

public record DeleteCustomerCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteCustomerCommandHandler(ICustomerRepository repository, IHttpContextAccessor httpContextAccessor) : BaseResponseHandler, IRequestHandler<DeleteCustomerCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken ct)
    {
        await repository.DeleteWhere(e => e.Id == request.Id && e.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()));
        return Success(true);
    }
}