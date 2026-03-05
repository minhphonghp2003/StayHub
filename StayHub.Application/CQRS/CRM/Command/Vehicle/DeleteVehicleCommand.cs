using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Vehicle;
public record DeleteVehicleCommand(int Id) : IRequest<BaseResponse<bool>>;
public sealed class DeleteVehicleCommandHandler(IVehicleRepository repository) : BaseResponseHandler, IRequestHandler<DeleteVehicleCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(DeleteVehicleCommand request, CancellationToken ct) 
    {
        await repository.Delete(new StayHub.Domain.Entity.CRM.Vehicle { Id = request.Id });
        return Success(true);
    }
}