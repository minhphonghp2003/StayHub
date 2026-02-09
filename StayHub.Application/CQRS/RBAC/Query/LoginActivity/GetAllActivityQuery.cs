using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;


namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    public record GetAllActivityQuery(int userId, int? pageNumber = null, int? pageSize = null) : IRequest<Response<LoginActivity>>;
    public sealed class GetAllActtivityQueryHandler(ILoginActivityRepository loginActivityRepository, IConfiguration _configuration) : BaseResponseHandler, IRequestHandler<GetAllActivityQuery, Response<LoginActivity>>
    {
        public async Task<Response<LoginActivity>> Handle(GetAllActivityQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? _configuration.GetValue<int>("PageSize");
            var (items, count) = await loginActivityRepository.GetAllPaginatedActivity(request.userId ,request.pageNumber ?? 1, pageSize);
            return SuccessPaginated<LoginActivity>(data: items, totalCount: count, pageSize: pageSize, currentPage: request.pageNumber ?? 1);
        }
    }

}
