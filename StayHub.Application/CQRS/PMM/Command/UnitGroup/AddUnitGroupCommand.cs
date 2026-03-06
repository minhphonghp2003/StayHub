using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Command.UnitGroup;
public record AddUnitGroupCommand(string Name,int PropertyId) : IRequest<BaseResponse<bool>>;
public sealed class AddUnitGroupCommandHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddUnitGroupCommand, BaseResponse<bool>> 
{
    public async Task<BaseResponse<bool>> Handle(AddUnitGroupCommand request, CancellationToken ct) 
    {
        var entity = new StayHub.Domain.Entity.PMM.UnitGroup { Name = request.Name,PropertyId = request.PropertyId };
        await repository.AddAsync(entity);
        return Success(true);
    }
}