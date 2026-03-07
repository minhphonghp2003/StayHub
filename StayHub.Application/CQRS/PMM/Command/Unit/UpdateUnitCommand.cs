using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using Microsoft.AspNetCore.Http;
using StayHub.Application.Extension;
namespace StayHub.Application.CQRS.PMM.Command.Unit;
public class UpdateUnitCommand : IRequest<BaseResponse<UnitDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string? Name { get; set; }
    public int UnitGroupId { get; set; }
    public decimal BasePrice { get; set; }
    public int MaximumCustomer { get; set; }

}
public sealed class UpdateUnitCommandHandler(IUnitRepository repository,IUnitGroupRepository unitGroupRepository, IHttpContextAccessor httpContextAccessor) 
    : BaseResponseHandler, IRequestHandler<UpdateUnitCommand, BaseResponse<UnitDTO>> 
{
    public async Task<BaseResponse<UnitDTO>> Handle(UpdateUnitCommand request, CancellationToken ct) 
    {
        if ((await unitGroupRepository.FindOneEntityAsync(e => e.Id == request.UnitGroupId && e.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()))) == null)
        {
            return Failure<UnitDTO>("Unauthorized", System.Net.HttpStatusCode.Unauthorized);
        }
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<UnitDTO>("Not found", HttpStatusCode.BadRequest);
        entity.Name = request.Name ?? entity.Name;
        entity.UnitGroupId = request.UnitGroupId;
        entity.BasePrice = request.BasePrice;
        entity.MaximumCustomer = request.MaximumCustomer;
        repository.Update(entity);
        return Success(new UnitDTO { Id = entity.Id, Name = entity.Name });
    }
}