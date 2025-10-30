using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Role
{
    // Include properties to be used as input for the command
    public record AddRoleCommand(string Code, string Name, string? Description) : IRequest<BaseResponse<RoleDTO>>;
    public sealed class AddRoleCommandHandler(IRoleRepository roleRepository) : BaseResponseHandler, IRequestHandler<AddRoleCommand, BaseResponse<RoleDTO>>
    {
        public async Task<BaseResponse<RoleDTO>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new StayHub.Domain.Entity.RBAC.Role
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
            };
            await roleRepository.AddAsync(role);
            return Success<RoleDTO>(new RoleDTO
            {
                Id = role.Id,
                Code = role.Code,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            }, null);
        }
    }

}