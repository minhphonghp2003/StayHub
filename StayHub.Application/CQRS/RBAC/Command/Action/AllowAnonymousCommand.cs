using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.Menu
{
    public record AllowAnonymousCommand(int Id, bool Activated) : IRequest<BaseResponse<bool>>;
    public sealed class AllowAnonymousCommandHandler(IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<AllowAnonymousCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(AllowAnonymousCommand request, CancellationToken cancellationToken)
        {
            return Success<bool>(await actionRepository.AllowAnonymous(request.Id, request.Activated));

        }
    }
}
