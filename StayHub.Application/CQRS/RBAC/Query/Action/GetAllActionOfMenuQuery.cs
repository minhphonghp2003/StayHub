using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Action
{
    // Include properties to be used as input for the query
    public record GetAllActionOfMenuQuery(int MenuId) : IRequest<BaseResponse<List<ActionDTO>>>;
    public sealed class GetAllActionOfMenuQueryHandler(IMenuActionRepository menuActionRepository) : BaseResponseHandler, IRequestHandler<GetAllActionOfMenuQuery, BaseResponse<List<ActionDTO>>>
    {
        public async Task<BaseResponse<List<ActionDTO>>> Handle(GetAllActionOfMenuQuery request, CancellationToken cancellationToken)
        {
            return Success(data: (await menuActionRepository.GetAllActionOfMenu(request.MenuId)).ToList());
        }
    }

}
