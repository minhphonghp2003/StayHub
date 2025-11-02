using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Query.Action
{
    // Include properties to be used as input for the query
    public record GetActionByIdQuery(int Id) : IRequest<BaseResponse<ActionDTO>>;
    internal sealed class GetActionByIdQueryHandler(IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<GetActionByIdQuery, BaseResponse<ActionDTO>>
    {
        public async Task<BaseResponse<ActionDTO>> Handle(GetActionByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await actionRepository.FindOneAsync(filter: e => e.Id == request.Id, selector: (e) => new ActionDTO
            {
                Id = e.Id,
                Path = e.Path,
                Method = e.Method,
                AllowAnonymous = e.AllowAnonymous,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            });
            if (result == null)
            {
                return Failure<ActionDTO>(message: "Action not found", code: System.Net.HttpStatusCode.BadRequest);
            }
            return Success<ActionDTO>(result);
        }
    }

}
