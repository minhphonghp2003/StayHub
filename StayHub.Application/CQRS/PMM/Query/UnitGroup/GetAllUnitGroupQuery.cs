using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.UnitGroup;
public record GetAllUnitGroupQuery(int propertyId, int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<UnitGroupDTO>>;
public sealed class GetAllUnitGroupQueryHandler(IUnitGroupRepository repository, IConfiguration config) 
    : BaseResponseHandler, IRequestHandler<GetAllUnitGroupQuery, Response<UnitGroupDTO>> 
{
    public async Task<Response<UnitGroupDTO>> Handle(GetAllUnitGroupQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => x.PropertyId == request.propertyId && request.searchKey == null || x.Name.Contains(request.searchKey),
            selector: (x, i) => new UnitGroupDTO { Id = x.Id, Name = x.Name }
        );
        return SuccessPaginated(result.ToList(),count, size, request.pageNumber ?? 1);
    }
}