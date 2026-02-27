using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.HRM;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.HRM.Query.Employee;

public record GetAllEmployeeQuery(int propertyId, int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<UserDTO>>;

public sealed class GetAllEmployeeQueryHandler(IUserRepository userRepository, IConfiguration configuration) 
    : BaseResponseHandler, IRequestHandler<GetAllEmployeeQuery, Response<UserDTO>>
{
    public async Task<Response<UserDTO>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.pageSize ?? configuration.GetValue<int>("PageSize");
         var inValidRoles = Enum.GetNames(typeof(SystemRole)).ToList(); 
        var (result, count) = await userRepository.GetManyPagedAsync(
            filter: x =>!x.UserRoles.Any(ur=>inValidRoles.Contains(ur.Role.Code)) && x.Properties.Any(e=>e.Id==request.propertyId) && string.IsNullOrEmpty(request.searchKey) || x.Id.ToString().Contains(request.searchKey),
            selector: (x, i) => new UserDTO
            {
                Id = x.Id,
                Username = x.Username,
                IsActive = x.IsActive,
                Fullname = x.Profile.Fullname,
                Email = x.Profile.Email,
                Phone = x.Profile.Phone,
                Image = x.Profile.Image,
            },
            include:e=>e.Include(j=>j.Profile),
            pageNumber: request.pageNumber??1,
            pageSize: pageSize
        );;
        return SuccessPaginated(result, count, pageSize, request.pageNumber ?? 1);
    }
}
