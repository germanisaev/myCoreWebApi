using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApi.Models
{
    public class AppSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
        public string Issuer_URL { get; set; }
    }
}
