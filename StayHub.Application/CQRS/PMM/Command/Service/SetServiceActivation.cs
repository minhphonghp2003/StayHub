using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Command.Service
{
    public record SetServiceActivationCommand(int serviceId, bool isActive) : IRequest<BaseResponse<bool>>;
    public sealed class SetServiceActivationCommandHandler(IServiceRepository repository)
        : BaseResponseHandler, IRequestHandler<SetServiceActivationCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(SetServiceActivationCommand request, CancellationToken ct)
        {
            var entity = await repository.GetEntityByIdAsync(request.serviceId);
            if (entity == null)
            {
                return Failure<bool>("No service found", System.Net.HttpStatusCode.BadRequest);
            }
            entity.IsActive = request.isActive;
            repository.Update(entity);
            return Success(true);
        }
    }
}
