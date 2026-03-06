using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Service;
public record AddServiceCommand(string Name,  int UnitTypeId,long Price, int PropertyId, bool IsActive, string? Description) : IRequest<BaseResponse<bool>>;
public sealed class AddServiceCommandHandler(IServiceRepository repository) : BaseResponseHandler, IRequestHandler<AddServiceCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddServiceCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.PMM.Service 
        { 
            Name = request.Name,
            PropertyId = request.PropertyId,
            UnitTypeId = request.UnitTypeId,
            Price = request.Price,
            IsActive = request.IsActive,
            Description = request.Description
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}