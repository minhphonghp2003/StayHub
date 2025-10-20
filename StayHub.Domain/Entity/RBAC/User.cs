namespace StayHub.Domain.Entity.RBAC
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public List<Token>? RefreshTokens { get; set; }
    }
}
