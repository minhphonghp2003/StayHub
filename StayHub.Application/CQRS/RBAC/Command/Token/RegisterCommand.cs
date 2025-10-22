using BCrypt.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Services;
using StayHub.Domain.Entity.RBAC;
using System.Net;

namespace StayHub.Application.CQRS.RBAC.Command.Token
{
    // Include properties to be used as input for the command
    public record RegisterCommand(String Username, String Password,String Email) : IRequest<BaseResponse<TokenDTO>>;
    public sealed class RegisterCommandHandler (IUserRepository userRepository,IJwtService tokenService ) :BaseResponseHandler, IRequestHandler<RegisterCommand, BaseResponse<TokenDTO>>
    {
        public async Task<BaseResponse<TokenDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.ExistsByUsernameAsync(request.Username);
            if(userExists)
            {
                return Failure<TokenDTO>("Username already exists", HttpStatusCode.Conflict);
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new User()
            {
                Username = request.Username,
                Password = hashedPassword,
                Email = request.Email
            };
            await userRepository.AddAsync(newUser);
            var token = await tokenService.GenerateJwtToken(newUser, new List<string>());
            return Success<TokenDTO>(new TokenDTO
            {
                Username = newUser.Username,
                Id = newUser.Id,
                Token = token.Item1,
                ExpiresDate = token.Item2
            }, "User registered successfully");
        }
    }

}