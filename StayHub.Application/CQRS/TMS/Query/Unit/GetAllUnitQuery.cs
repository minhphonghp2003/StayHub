using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.Unit;

public record GetAllUnitQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<UnitDTO>>;

public sealed class GetAllUnitQueryHandler(IUnitRepository repository, IConfiguration configuration) 
    : BaseResponseHandler, IRequestHandler<GetAllUnitQuery, Response<UnitDTO>>
{
    public async Task<Response<UnitDTO>> Handle(GetAllUnitQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.pageSize ?? configuration.GetValue<int>("PageSize");
        var (result, count) = await repository.GetAllUnitPaginated(request.pageNumber ?? 1, pageSize, request.searchKey);
        return SuccessPaginated(result, count, pageSize, request.pageNumber ?? 1);
    }
}
