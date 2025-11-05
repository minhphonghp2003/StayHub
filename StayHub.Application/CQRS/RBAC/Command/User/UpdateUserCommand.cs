using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.User
{
    public class UpdateUserCommand : IRequest<BaseResponse<ProfileDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        public UpdateUserCommand(int id, string email)
        {
            Id = id;
            Email = email;
        }

        // Parameterless constructor for model binding / deserialization
        public UpdateUserCommand() { }
    }
    public sealed class UpdateUserCommandCommandHandler(IUserRepository menuRepository) : BaseResponseHandler, IRequestHandler<UpdateUserCommand, BaseResponse<ProfileDTO>>
    {
        public async Task<BaseResponse<ProfileDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new StayHub.Domain.Entity.RBAC.User
            {
                Id = request.Id,
            };
            menuRepository.Update(user);
            return Success<ProfileDTO>(new ProfileDTO
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            });

        }
    }
}
