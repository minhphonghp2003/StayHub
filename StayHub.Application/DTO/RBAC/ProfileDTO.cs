using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.RBAC
{
    public class ProfileDTO:BaseDTO
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public List<RoleDTO>? Roles { get; set; }
    }
}
