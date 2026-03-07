using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.Unit;

public record AddUnitCommand(string Name, int UnitGroupId, long BasePrice, int MaximumCustomer, bool IsActive) : IRequest<BaseResponse<bool>>;
public sealed class AddUnitCommandHandler(IUnitRepository repository,IUnitGroupRepository unitGroupRepository,IHttpContextAccessor httpContextAccessor)
    : BaseResponseHandler, IRequestHandler<AddUnitCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddUnitCommand request, CancellationToken ct)
    {
        if((await unitGroupRepository.FindOneEntityAsync(e=>e.Id==request.UnitGroupId&& e.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()))) == null)
        {
            return Failure<bool>("Unauthorized", System.Net.HttpStatusCode.Unauthorized);
        }
        var entity = new StayHub.Domain.Entity.PMM.Unit
        {
            Name = request.Name,
            BasePrice = request.BasePrice,
            UnitGroupId = request.UnitGroupId,
            MaximumCustomer = request.MaximumCustomer,
            IsActive = request.IsActive,
            Status = UnitStatus.AVAILABLE
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}