using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Vehicle;
public class UpdateVehicleCommand : IRequest<BaseResponse<VehicleDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string LicensePlate { get; set; }
    public string? Image { get; set; }
}
public sealed class UpdateVehicleCommandHandler(IVehicleRepository repository) : BaseResponseHandler, IRequestHandler<UpdateVehicleCommand, BaseResponse<VehicleDTO>> 
{
    public async Task<BaseResponse<VehicleDTO>> Handle(UpdateVehicleCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<VehicleDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.CustomerId = request.CustomerId;
        entity.Name = request.Name;
        entity.LicensePlate = request.LicensePlate;
        entity.Image = request.Image;

        repository.Update(entity);
        return Success(new VehicleDTO 
        { 
            Id = entity.Id, 
            CustomerId = entity.CustomerId,
            Name = entity.Name,
            LicensePlate = entity.LicensePlate,
            Image = entity.Image
        });
    }
}