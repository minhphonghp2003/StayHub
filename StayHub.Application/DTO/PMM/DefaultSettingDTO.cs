using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.PMM
{
    public class DefaultSettingDTO
    {
        public DateTime? DefaultPaymentDate { get; set; }
        public long? DefaultBasePrice { get; set; }
    }
}
