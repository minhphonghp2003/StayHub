using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    // Include properties to be used as input for the query
    public record GetAllUserQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<UserDTO>>;
    public sealed class GetAllUserQueryHandler(IUserRepository userRepository, IConfiguration configuration) : BaseResponseHandler, IRequestHandler<GetAllUserQuery, Response<UserDTO>>
    {
        public async Task<Response<UserDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? configuration.GetValue<int>("PageSize");
            var (items, totalCount) = await userRepository.GetAllUser(request.pageNumber??1, pageSize, request.searchKey);
            return SuccessPaginated(items,totalCount,pageSize, request.pageNumber ?? 1);
        }
    }

}
