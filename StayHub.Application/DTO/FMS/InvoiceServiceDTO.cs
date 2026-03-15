using StayHub.Domain.Entity.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.FMS
{
    public class InvoiceServiceDTO
    {
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        public double Quantity { get; set; }

    }
}
