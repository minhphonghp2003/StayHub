using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Domain.Entity.Catalog
{
    [Index(nameof(Code), IsUnique = true)]
    public class CategoryItem :BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Value { get; set; }
        public string? Icon { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
