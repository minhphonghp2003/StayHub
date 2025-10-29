using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.RBAC
{
    public class AllActionDTO
    {
        public string Path { get; set; }
        public IEnumerable<string?> Methods { get; set; }
    }
}
