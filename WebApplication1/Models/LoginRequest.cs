using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication1.Models
{
    public class LoginRequest
    {
        public string CompanyDB { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public string ServiceLayerUrl { get; set; }
    }
}
