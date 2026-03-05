using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Vehicle;
public record AddVehicleCommand(int CustomerId, string Name, string LicensePlate, string? Image) : IRequest<BaseResponse<bool>>;
public sealed class AddVehicleCommandHandler(IVehicleRepository repository) : BaseResponseHandler, IRequestHandler<AddVehicleCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddVehicleCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.CRM.Vehicle 
        { 
            CustomerId = request.CustomerId,
            Name = request.Name,
            LicensePlate = request.LicensePlate,
            Image = request.Image
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}