using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.HRM;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.HRM.Command.Employee;

public record DeleteEmployeeCommand(int Id,int propertyId) : IRequest<BaseResponse<bool>>;

public sealed class DeleteEmployeeCommandHandler(IUserRepository userRepository) 
    : BaseResponseHandler, IRequestHandler<DeleteEmployeeCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindOneEntityAsync(filter: e => e.Id == request.Id,
            include: e => e.Include(j => j.Properties).Include(j => j.Profile));
        if (user == null)
        {
            return Failure<bool>("Employee not found", System.Net.HttpStatusCode.BadRequest);
        }
        user.Properties.RemoveAll(p => p.Id == request.propertyId);
        userRepository.Update(user);
        return Success(true);
    }
}
