using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.DTO.RBAC
{
    public class JWKDTO
    {
        public string kty { get; set; }
        public string use { get; set; }
        public string kid { get; set; }
        public string alg { get; set; }
        public string n  { get; set; }
        public string e  { get; set; }
    }
    public class JWKSetDTO
    {
        public List<JWKDTO> keys { get; set; }
    }
}
