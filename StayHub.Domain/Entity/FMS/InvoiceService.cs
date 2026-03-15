using Microsoft.EntityFrameworkCore;
using StayHub.Domain.Entity.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Domain.Entity.FMS
{
    public class InvoiceService :BaseEntity
    {
        public int InvoiceId { get; set; }
        public int ServiceId { get; set; }
        public double Quantity { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Service Service { get; set; }
    }
}
