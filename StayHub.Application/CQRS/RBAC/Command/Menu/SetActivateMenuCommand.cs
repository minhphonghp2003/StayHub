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
    public record SetActivateMenuCommand(int Id, bool Activated) : IRequest<BaseResponse<bool>>;
    public sealed class SetActivateMenuCommandHandler(IMenuRepository actionRepository) : BaseResponseHandler, IRequestHandler<SetActivateMenuCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(SetActivateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = new StayHub.Domain.Entity.RBAC.Menu
            {
                Id = request.Id,
                IsActive = request.Activated
            };
            actionRepository.Update(menu);
            return Success<bool>(request.Activated
         );

        }
    }
}
