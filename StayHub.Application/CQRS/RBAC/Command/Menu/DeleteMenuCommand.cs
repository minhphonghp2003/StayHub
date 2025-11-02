using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.Menu
{
    public record DeleteMenuCommand(int Id) : IRequest<BaseResponse<bool>>;
    public sealed class DeleteMenuCommandHandler(IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<DeleteMenuCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            await menuRepository.Delete(new Domain.Entity.RBAC.Menu { Id = request.Id });
            return Success(true);
        }
    }

}
