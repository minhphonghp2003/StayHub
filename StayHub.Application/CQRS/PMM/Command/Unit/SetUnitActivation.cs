using MediatR;
using Shared.Common;
using Shared.Response;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Command.Unit
{
    public record SetUnitActivationCommand(int unitId,bool isActive) : IRequest<BaseResponse<bool>>;
    public sealed class SetUnitActivationCommandHandler(IUnitRepository repository)
        : BaseResponseHandler, IRequestHandler<SetUnitActivationCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(SetUnitActivationCommand request, CancellationToken ct)
        {
            var entity = await repository.GetEntityByIdAsync(request.unitId);
            if (entity == null)
            {
                return Failure<bool>("No unit found",System.Net.HttpStatusCode.BadRequest);
            }
            entity.IsActive = request.isActive;
            repository.Update(entity);
            return Success(true);
        }
    }
}
