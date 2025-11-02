using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.Role
{
    public class UpdateRoleCommand : IRequest<BaseResponse<RoleDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public UpdateRoleCommand(int id, string name, string description, string code)
        {
            Id = id;
            Name = name;
            Description = description;
            Code = code;
        }

        // Parameterless constructor for model binding or deserialization
        public UpdateRoleCommand() { }
    }
    public sealed class UpdateRoleCommandCommandHandler(IRoleRepository actionRepository) : BaseResponseHandler, IRequestHandler<UpdateRoleCommand, BaseResponse<RoleDTO>>
    {
        public async Task<BaseResponse<RoleDTO>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new StayHub.Domain.Entity.RBAC.Role
            {
                Id = request.Id,
                Code = request.Code,
                Name = request.Name,
                Description = request.Description
            };
            actionRepository.Update(role);
            return Success<RoleDTO>(new RoleDTO
            {
                Id = role.Id,
                Code = role.Code,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            });

        }
    }
}
