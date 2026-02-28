using BCrypt.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Services;
using StayHub.Domain.Entity.RBAC;
using System.Net;
using Microsoft.AspNetCore.Http;
using StayHub.Application.Extension;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record RegisterCommand(String Fullname, String Username, String Password, String Email)
        : IRequest<BaseResponse<bool>>;

    public sealed class RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtService tokenService,
        ITokenRepository tokenRepository,
        IHttpContextAccessor accessor,
        IAuthService authService) : BaseResponseHandler, IRequestHandler<RegisterCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            
            bool userExists = await userRepository.ExistsByUsernameAsync(request.Username);
            if (userExists)
            {
                return Failure<bool>("Username already exists", HttpStatusCode.Conflict);
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new Domain.Entity.RBAC.User
            {
                Username = request.Username,
                Password = hashedPassword,
            };
            var profile = new Profile
            {
                Fullname = request.Fullname,
                Email = request.Email,
            };
            newUser.Profile = profile;
            await userRepository.AddAsync(newUser);
            var token = await tokenService.GenerateJwtToken(newUser, new List<string>());
            var (refreshToken, expires) = await authService.GenerateRefreshToken(userId: newUser.Id);
            return Success<bool>(true, "User registered successfully");
        }
    }
}