using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.Vehicle;
public record GetVehicleByIdQuery(int Id) : IRequest<BaseResponse<VehicleDTO>>;
public sealed class GetVehicleByIdQueryHandler(IVehicleRepository repository) : BaseResponseHandler, IRequestHandler<GetVehicleByIdQuery, BaseResponse<VehicleDTO>> 
{
    public async Task<BaseResponse<VehicleDTO>> Handle(GetVehicleByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new VehicleDTO 
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
        });
        return result == null ? Failure<VehicleDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}