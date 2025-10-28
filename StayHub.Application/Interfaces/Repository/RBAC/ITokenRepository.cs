using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface ITokenRepository : IRepository<Token>
    {
        public Task<Token?> GetTokenInfo(string refreshToken);
    }
}
