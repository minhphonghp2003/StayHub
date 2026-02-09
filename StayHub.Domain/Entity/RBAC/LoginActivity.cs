namespace StayHub.Domain.Entity.RBAC;

public class LoginActivity : BaseEntity
{
    public DateTime Time { get; set; }
    public String OS { get; set; }
    public String IP { get; set; }  
    public int UserId { get; set; }
    public bool Status { get; set; }
    public String Browser { get; set; }
    
}