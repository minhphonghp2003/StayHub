using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.Unit;

//public UnitStatus Status { get; set; }
//public decimal BasePrice { get; set; }
//public int MaximumCustomer { get; set; }
//public bool IsActive { get; set; }

//public UnitGroupDTO UnitGroup { get; set; }
public record GetAllUnitQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<UnitDTO>>;
public sealed class GetAllUnitQueryHandler(IUnitRepository repository, IConfiguration config)
    : BaseResponseHandler, IRequestHandler<GetAllUnitQuery, Response<UnitDTO>>
{
    public async Task<Response<UnitDTO>> Handle(GetAllUnitQuery request, CancellationToken ct)
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null || x.Name.Contains(request.searchKey),
            include:x=>x.Include(j=>j.UnitGroup),
            selector: (x, i) => new UnitDTO
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                BasePrice = x.BasePrice,
                MaximumCustomer = x.MaximumCustomer,
                IsActive = x.IsActive,
                UnitGroup = new UnitGroupDTO
                {
                    Id = x.Id,
                    Name = x.Name,

                } 
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}