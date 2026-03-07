using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Command.Job
{
    public record SetJobActivationCommand(int jobId, bool isActive) : IRequest<BaseResponse<bool>>;
    public sealed class SetJobActivationCommandHandler(IJobRepository repository)
        : BaseResponseHandler, IRequestHandler<SetJobActivationCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(SetJobActivationCommand request, CancellationToken ct)
        {
            var entity = await repository.GetEntityByIdAsync(request.jobId);
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
