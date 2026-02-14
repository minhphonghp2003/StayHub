using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.UnitGroup;

public record GetAllUnitGroupQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<UnitGroupDTO>>;

public sealed class GetAllUnitGroupQueryHandler(IUnitGroupRepository repository, IConfiguration configuration) 
    : BaseResponseHandler, IRequestHandler<GetAllUnitGroupQuery, Response<UnitGroupDTO>>
{
    public async Task<Response<UnitGroupDTO>> Handle(GetAllUnitGroupQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.pageSize ?? configuration.GetValue<int>("PageSize");
        var (result, count) = await repository.GetAllUnitGroupPaginated(request.pageNumber ?? 1, pageSize, request.searchKey);
        return SuccessPaginated(result, count, pageSize, request.pageNumber ?? 1);
    }
}
