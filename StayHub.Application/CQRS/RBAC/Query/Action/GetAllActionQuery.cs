using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Action
{
    // Include properties to be used as input for the query
    public record GetAllActionQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<ActionDTO>>;
    public sealed class GetAllActionQueryHandler(IActionRepository actionRepository, IConfiguration configuration) : BaseResponseHandler, IRequestHandler<GetAllActionQuery, Response<ActionDTO>>
    {
        public async Task<Response<ActionDTO>> Handle(GetAllActionQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.pageSize ?? configuration.GetValue<int>("PageSize");
            var (result,count) = await actionRepository.GetManyPagedAsync(
                pageNumber: request.pageNumber ?? 1,
                pageSize: pageSize,
                filter: x => request.searchKey == null || x.Path.Contains(request.searchKey),
                selector: (x, i) => new ActionDTO
                {
                    Id = x.Id,
                    Path = x.Path,
                    Method = x.Method,
                    AllowAnonymous = x.AllowAnonymous
                }
                );
            return SuccessPaginated(result, count, pageSize, request.pageNumber??1);
        }
    }

}
