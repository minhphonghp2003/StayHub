using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.Catalog
{
    public class CategoryItemDTO : BaseDTO
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Value { get; set; }
        public string? Icon { get; set; }
        public int? CategoryId { get; set; }
    }
}
