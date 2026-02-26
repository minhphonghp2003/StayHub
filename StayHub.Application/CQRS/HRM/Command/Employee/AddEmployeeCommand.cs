using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Interfaces.Repository.HRM;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Repository.TMS;
using StayHub.Domain.Entity.RBAC;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Application.CQRS.HRM.Command.Employee;

public record AddEmployeeCommand(
    int propertyId,
    String fullname,
    String username,
    String password,
    List<int> roleIds,
    int? id)
    : IRequest<BaseResponse<bool>>;

public sealed class AddEmployeeCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository,IPropertyRepository propertyRepository)
    : BaseResponseHandler, IRequestHandler<AddEmployeeCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = request.id != null
            ? await userRepository.FindOneEntityAsync(filter: e => e.Id == request.id,
                include: e => e.Include(j => j.Properties).Include(j => j.Profile),trackChange:true)
            : null;
        var property = await propertyRepository.FindOneEntityAsync(filter:e=>e.Id==request.propertyId);

        if (property == null)
        {
            return Failure<bool>("Property not found", System.Net.HttpStatusCode.BadRequest);
        }
         if (user != null)
        {
            if(user.Properties.Any(p=>p.Id==request.propertyId))
            {
                return Failure<bool>("Employee already exists in this property", System.Net.HttpStatusCode.BadRequest);
            }
            user.Properties.Add(property);
            await userRepository.SaveAsync();
        }
        else
        {
            var newUser = new Domain.Entity.RBAC.User
            {
                Username = request.username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.password)
            };
            var profile = new Domain.Entity.RBAC.Profile
            {
                Fullname = request.fullname,
            };
            var inValidRoles = Enum.GetNames(typeof(SystemRole)).ToList();
            var roles = await roleRepository.GetManyAsync(
                filter: r => request.roleIds.Contains(r.Id) && !inValidRoles.Contains(r.Code),
                selector: (e, i) => e.Id);
            newUser.Profile = profile;
            newUser.Properties.Add(property);
            newUser.UserRoles.AddRange(roles.Select(roleId => new UserRole
            {
                RoleId = roleId
            }));
            await userRepository.AddAsync(newUser);
             
        }

        return Success(true);
    }
}