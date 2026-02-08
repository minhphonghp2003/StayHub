using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Command.User
{
    public class UpdateUserCommand : IRequest<BaseResponse<ProfileDTO>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }

        public UpdateUserCommand(int id, string fullname, string email, string? phone = null, string? address = null, string? image = null)
        {
            Id = id;
            Fullname = fullname;
            Email = email;
            Phone = phone;
            Address = address;
            Image = image;
        }

        // Parameterless constructor for model binding / deserialization
        public UpdateUserCommand() { }
    }
    public sealed class UpdateUserCommandCommandHandler(IUserRepository userRepository) : BaseResponseHandler, IRequestHandler<UpdateUserCommand, BaseResponse<ProfileDTO>>
    {
        public async Task<BaseResponse<ProfileDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await  userRepository.GetEntityByIdAsync(request.Id);
            if (user == null)
            {
                return Failure<ProfileDTO>("User not found", System.Net.HttpStatusCode.NotFound);
            }
            user.Profile.Fullname =  request.Fullname;
            user.Profile.Email = request.Email;
            user.Profile.Phone = request.Phone;
            user.Profile.Address = request.Address;
            user.Profile.Image = request.Image;
            userRepository.Update(user);
            
            return Success<ProfileDTO>(new ProfileDTO
            {
                Id = user.Id,
                Username = user.Username,
                Fullname = user.Profile.Fullname,
                Email = user.Profile.Email,
                Phone = user.Profile.Phone,
                Address = user.Profile.Address,
                Image = user.Profile.Image,
                Roles = user.UserRoles.Select(ur => new RoleDTO
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name,
                }).ToList(),
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            });

        }
    }
}
