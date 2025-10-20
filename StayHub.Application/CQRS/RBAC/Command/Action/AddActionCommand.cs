using MediatR;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System.Net;

namespace StayHub.Application.CQRS.RBAC.Command.Action
{
    // Include properties to be used as input for the command
    public record AddActionCommand(string Path, HttpVerb Method, bool AllowAnon) : IRequest<BaseResponse<ActionDTO>>;
    public class AddActionCommandHandler : BaseResponseHandler, IRequestHandler<AddActionCommand, BaseResponse<ActionDTO>>
    {
        private readonly IActionRepository _actionRepository;
        public AddActionCommandHandler(IActionRepository repository)
        {
            _actionRepository = repository;

        }
        public async Task<BaseResponse<ActionDTO>> Handle(AddActionCommand request, CancellationToken cancellationToken)
        {
            Domain.Entity.RBAC.Action action = new Domain.Entity.RBAC.Action
            {
                Path = request.Path,
                AllowAnonymous = request.AllowAnon,
                Method = request.Method

            };
            await _actionRepository.AddAsync(action);
            return Success(new ActionDTO
            {
                Path = request.Path,
                AllowAnonymous = request.AllowAnon,
                Id = action.Id,
                CreatedAt = action.CreatedAt,
                UpdatedAt = action.UpdatedAt,
                Method = request.Method
            },null,HttpStatusCode.Created);



        }
    }

}