using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Commons
{
    public class AppSetting
    {
        public string? JwtSecret { get; set; }
        public string? JwtIssuer { get; set; }
        public string? JwtAudience { get; set; }
    }
}
