using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StayHub.Domain.Entity.Catalog
{
    [Index(nameof(Code), IsUnique = true)]
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public virtual List<CategoryItem>? CategoryItems { get; set; }
    }
}
