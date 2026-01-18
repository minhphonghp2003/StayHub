using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    public record GetAllUserOfRoleQuery(int roleId, int? pageNumber, int? pageSize ) : IRequest<Response<UserDTO>>;
    public sealed class GetAllUserOfRoleQueryHandler(IUserRepository userRepository,IConfiguration configuration) : BaseResponseHandler, IRequestHandler<GetAllUserOfRoleQuery, Response<UserDTO>>
    {
        public async Task<Response<UserDTO>> Handle(GetAllUserOfRoleQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? configuration.GetValue<int>("PageSize");
            var (result,count) =await userRepository.GetUserOfRole(request.roleId, request.pageNumber ?? 1, pageSize);
            return SuccessPaginated(data:result,totalCount:count,pageSize:pageSize,currentPage  :request.pageNumber??1 );
        }
    }
}
