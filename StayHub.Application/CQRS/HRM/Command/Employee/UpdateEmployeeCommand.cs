using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.HRM;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Application.CQRS.HRM.Command.Employee;

public record UpdateEmployeeCommand
    : IRequest<BaseResponse<bool>>
{
    [JsonIgnore] public int Id { get; set; }

    public int propertyId;
    public int userId;
    public string fullname;
    public string username;
    public string password;
    public List<int> roleIds;

    public UpdateEmployeeCommand()
    {
    }

    public UpdateEmployeeCommand(int userId, int propertyId, string fullname, string username, string password,
        List<int> roleIds)
    {
        this.propertyId = propertyId;
        this.fullname = fullname;
        this.username = username;
        this.password = password;
        this.roleIds = roleIds;
        this.userId = userId;
    }
};

public sealed class UpdateEmployeeCommandHandler(IUserRepository userRepository, IHttpContextAccessor accessor)
    : BaseResponseHandler, IRequestHandler<UpdateEmployeeCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindOneEntityAsync(
            filter: e =>
                e.Id == request.userId && e.Properties.Any(p => p.Id == request.propertyId) &&
                e.CreatedByUserId == accessor.HttpContext.GetUserId(),
            include: e => e.Include(j => j.Properties).Include(j => j.Profile), trackChange: true);
        if (user == null)
        {
            return Failure<bool>("Employee not found in this property", System.Net.HttpStatusCode.BadRequest);
        }

        if (!string.IsNullOrWhiteSpace(request.username))
        {
            user.Username = request.username;
        }

        if (!string.IsNullOrWhiteSpace(request.password))
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.password);
        }

        if (user.Profile != null)
        {
            user.Profile.Fullname = request.fullname;
        }
        else
        {
            user.Profile = new Domain.Entity.RBAC.Profile { Fullname = request.fullname };
        }

        user.UserRoles?.Clear();
        foreach (var roleId in request.roleIds)
        {
            user.UserRoles.Add(new UserRole
            {
                RoleId = roleId,
                UserId = user.Id
            });
        }

        await userRepository.SaveAsync();
        return Success(true);
    }
}