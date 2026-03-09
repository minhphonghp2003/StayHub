using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
using StayHub.Application.Interfaces.Repository.PMM;

namespace StayHub.Application.CQRS.CRM.Command.Contract
{
    public record ChangeRoomCommand(int contractId, int unitId) : IRequest<BaseResponse<bool>>;
    public sealed class ChangeRoomCommandHandler(IUnitRepository unitRepository, IContractRepository contractRepository) : BaseResponseHandler, IRequestHandler<ChangeRoomCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(ChangeRoomCommand request, CancellationToken cancellationToken)
        {

            var isUnitTaken = (await contractRepository.FindOneEntityAsync(e => e.UnitId == request.unitId)) != null;
            if (isUnitTaken)
            {
                return Failure<bool>("Phòng đang có người ở", System.Net.HttpStatusCode.BadRequest);
            }
            var contract = await contractRepository.GetEntityByIdAsync(request.contractId);
            if (contract == null)
            {
                return Failure<bool>("Hợp đồng không hợp lệ", System.Net.HttpStatusCode.BadRequest);
            }
            var propertyId = contract?.Unit.UnitGroup.PropertyId;
            int? propertyOfNewUnit = (await unitRepository.GetEntityByIdAsync(request.unitId))?.UnitGroup.PropertyId;
            if (propertyOfNewUnit == null || propertyId == null || propertyOfNewUnit != propertyId || contract.UnitId==request.unitId)
            {
                return Failure<bool>("Chuyển phòng không thành công", System.Net.HttpStatusCode.BadRequest);
            }
            contract.UnitId = request.unitId;
            await contractRepository.SaveAsync();
            return Success(true);

        }
    }

}