using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using Shared.Common;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Unit;

public class UpdateUnitCommand : IRequest<BaseResponse<UnitDTO>>
{
    [JsonIgnore]
    public int Id { get; set; }
        public int UnitGroupId { get; set; }
        public decimal BasePrice { get; set; }
        public UnitStatus Status { get; set; }
        public string Name  { get; set; }

        public UpdateUnitCommand()
        {
        }

        public UpdateUnitCommand(int unitGroupId, decimal basePrice, UnitStatus status, string name)
        {
            UnitGroupId = unitGroupId;
            BasePrice = basePrice;
            Status = status;
            Name = name;
        }
}

public sealed class UpdateUnitCommandHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateUnitCommand, BaseResponse<UnitDTO>>
{
    public async Task<BaseResponse<UnitDTO>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<UnitDTO>("Not found", HttpStatusCode.BadRequest);
        entity.UnitGroupId = request.UnitGroupId;
        entity.BasePrice = request.BasePrice;
        entity.Status = request.Status;
        entity.Name = request.Name;
        repository.Update(entity);
        return Success(new UnitDTO { Id = entity.Id });
    }
}
