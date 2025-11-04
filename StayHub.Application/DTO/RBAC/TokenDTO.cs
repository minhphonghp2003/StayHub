using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.RBAC
{
    public class TokenDTO : BaseDTO
    {
        public string? Email { get; set; }
        public string? Fullname { get; set; }
        public string? Image { get; set; }
        public int Id { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiresDate { get; set; }
    }
}
