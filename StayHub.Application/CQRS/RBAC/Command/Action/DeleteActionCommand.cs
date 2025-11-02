using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Action
{
    // Include properties to be used as input for the command
    public record DeleteActionCommand(int Id) : IRequest<BaseResponse<bool>>;
    public sealed class DeleteActionCommandHandler(IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<DeleteActionCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
        {
            await actionRepository.Delete(new Domain.Entity.RBAC.Action { Id = request.Id });
            return  Success(true);
        }
    }

}