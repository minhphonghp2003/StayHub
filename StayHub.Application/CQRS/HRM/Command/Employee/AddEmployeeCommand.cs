using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.HRM;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Repository.TMS;
using StayHub.Domain.Entity.RBAC;
using StayHub.Domain.Entity.TMS;

namespace StayHub.Application.CQRS.HRM.Command.Employee;

public record AddEmployeeCommand(
    int propertyId,
    String? fullname,
    String? username,
    String? password,
    String? email,
    int? id)
    : IRequest<BaseResponse<bool>>;

public sealed class AddEmployeeCommandHandler(
    IUserRepository userRepository,
    IHttpContextAccessor accessor,
    IPropertyRepository propertyRepository)
    : BaseResponseHandler, IRequestHandler<AddEmployeeCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = accessor.HttpContext.GetUserId();
        var user = request.id != null
            ? await userRepository.FindOneEntityAsync(filter: e => e.Id == request.id,
                include: e => e.Include(j => j.Properties).Include(j => j.Profile), trackChange: true)
            : null;
        var property = await propertyRepository.FindOneEntityAsync(filter: e => e.Id == request.propertyId,trackChange:true);

        if (property == null)
        {
            return Failure<bool>("Property not found", System.Net.HttpStatusCode.BadRequest);
        }

        if (user != null)
        {
            if (!user.IsActive)
            {
                return Failure<bool>("Employee is inactive, please contact administrator", System.Net.HttpStatusCode.BadRequest);
            }
            if (user.CreatedByUserId != currentUserId)
            {
                return Failure<bool>("You don't have permission to add this employee to the property",
                    System.Net.HttpStatusCode.Forbidden);
            }

            if (user.Properties.Any(p => p.Id == request.propertyId))
            {
                return Failure<bool>("Employee already exists in this property", System.Net.HttpStatusCode.BadRequest);
            }

            user.Properties.Add(property);
            await userRepository.SaveAsync();
        }
        else
        {
            if(request.username==null || request.email==null || request.password == null || request.fullname == null)
            {
                return Failure<bool>("Missing required fields for new employee", System.Net.HttpStatusCode.BadRequest);
            }
            var newUser = new User
            {
                Username = request.username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.password),
                CreatedByUserId = currentUserId,
                Properties = new  List<Property>()
            };
            var profile = new Profile
            {
                Fullname = request.fullname,
                Email = request.email,
            };
            newUser.Profile = profile;
            newUser.Properties.Add(property);
            await userRepository.AddAsync(newUser);
        }

        return Success(true);
    }
}