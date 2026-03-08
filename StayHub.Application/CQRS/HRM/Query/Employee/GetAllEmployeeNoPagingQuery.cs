using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.HRM;
using StayHub.Application.DTO.PMM;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.HRM;
using StayHub.Application.Interfaces.Repository.PMM;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.HRM.Query.Employee
{
    public record GetAllEmployeeNoPagingQuery(int propertyId) : IRequest<BaseResponse<List<UserDTO>>>;
    public sealed class GetAllEmployeeNoPagingQueryHandler(IUserRepository repository) : BaseResponseHandler, IRequestHandler<GetAllEmployeeNoPagingQuery, BaseResponse<List<UserDTO>>>
    {
        public async Task<BaseResponse<List<UserDTO>>> Handle(GetAllEmployeeNoPagingQuery request, CancellationToken ct)
        {
            var systemRoles = Enum.GetNames(typeof(SystemRole)).ToList();
            var result = await repository.GetManyAsync(
                filter: x => !x.UserRoles.Any(ur => systemRoles.Contains(ur.Role.Code)) && x.Properties.Any(p => p.Id == request.propertyId),
                include: x => x.Include(e => e.Profile),
                selector: (x, i) => new UserDTO
                {
                    Id = x.Id,
                    Fullname  = x.Profile.Fullname,
                    Phone = x.Profile.Phone,
                    Username = x.Username
                }
            );
            return Success(result.ToList());
        }
    }
}
