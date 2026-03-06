using MediatR;
using Shared.Common;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Unit;

public record AddUnitCommand(string Name, int UnitGroupId, long BasePrice, int MaximumCustomer, bool IsActive) : IRequest<BaseResponse<bool>>;
public sealed class AddUnitCommandHandler(IUnitRepository repository)
    : BaseResponseHandler, IRequestHandler<AddUnitCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddUnitCommand request, CancellationToken ct)
    {
        var entity = new StayHub.Domain.Entity.PMM.Unit { Name = request.Name, BasePrice = request.BasePrice, UnitGroupId = request.UnitGroupId, MaximumCustomer = request.MaximumCustomer, IsActive = request.IsActive };
        await repository.AddAsync(entity);
        return Success(true);
    }
}