using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.RBAC
{
   public class ActionDTO :BaseDTO
    {
        public string Path { get; set; }
        public HttpVerb Method { get; set; }
        public bool? AllowAnonymous { get; set; }
    }
}
