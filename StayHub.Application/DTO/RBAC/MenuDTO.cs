using StayHub.Domain.Entity.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.RBAC
{
    public class MenuDTO :BaseDTO
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }
        public int? GroupId { get; set; }
        public List<MenuDTO> Children { get; set; }
    }
    public class MenuGroupDTO
    {
        public string Name { get; set; }
        public List<MenuDTO> Items { get; set; }
    }
}
