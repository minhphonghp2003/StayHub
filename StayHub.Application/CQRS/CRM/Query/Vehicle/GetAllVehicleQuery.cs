using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using Microsoft.EntityFrameworkCore;
namespace StayHub.Application.CQRS.CRM.Query.Vehicle;
public record GetAllVehicleQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<VehicleDTO>>;
public sealed class GetAllVehicleQueryHandler(IVehicleRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllVehicleQuery, Response<VehicleDTO>> 
{
    public async Task<Response<VehicleDTO>> Handle(GetAllVehicleQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null || x.Name.Contains(request.searchKey) || x.Customer.Name.Contains(request.searchKey)|| x.Customer.Phone.Contains(request.searchKey),
            include: x=>x.Include(j=>j.Customer),
            selector: (x, i) => new VehicleDTO 
            { 
                Id = x.Id, 
                CustomerId = x.CustomerId,
                Customer = new CustomerDTO
                {
                    Id = x.Customer.Id,
                    Name = x.Customer.Name,
                },
                Name = x.Name,
                LicensePlate = x.LicensePlate,
                Image = x.Image
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}