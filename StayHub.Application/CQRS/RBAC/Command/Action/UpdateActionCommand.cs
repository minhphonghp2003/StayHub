using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using System.Text.Json.Serialization;

namespace StayHub.Application.CQRS.RBAC.Command.Action
{
    // Include properties to be used as input for the command
    public class UpdateActionCommand : IRequest<BaseResponse<ActionDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Path { get; set; } = string.Empty;

        public string Method { get; set; } = string.Empty;

        public bool AllowAnonymous { get; set; }

        public UpdateActionCommand(int id, string path, string method, bool allowAnonymous)
        {
            Id = id;
            Path = path;
            Method = method;
            AllowAnonymous = allowAnonymous;
        }

        // Optional parameterless constructor (needed for model binding in controllers)
    }
    public sealed class UpdateActionCommandCommandHandler(IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<UpdateActionCommand, BaseResponse<ActionDTO>>
    {
        public async Task<BaseResponse<ActionDTO>> Handle(UpdateActionCommand request, CancellationToken cancellationToken)
        {
            var action = new StayHub.Domain.Entity.RBAC.Action
            {
                Id = request.Id,
                Path = request.Path,
                Method = request.Method,
                AllowAnonymous = request.AllowAnonymous
            };
            actionRepository.Update(action);
            return Success<ActionDTO>(new ActionDTO
            {
                Id = action.Id,
                Path = action.Path,
                Method = action.Method,
                AllowAnonymous = action.AllowAnonymous,
                CreatedAt = action.CreatedAt,
                UpdatedAt = action.UpdatedAt
            });

        }
    }

}