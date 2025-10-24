using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Commons;

namespace UserApp.Service.Services.Roles.Dtos
{
    public class RoleGridDto: BaseDto
    {
        public string Description { get; set; }
        public string Application { get; set; }

        public RoleGridDto() { }
    }
}
