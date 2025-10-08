using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Commons;

namespace UserApp.Service.Services.RolesScreens.Dtos
{
    public class RoleScreenGridDto: BaseDto
    {
        public int RoleId { get; set; }
        public string Role { get; set; }
        public int ScreenId { get; set; }
        public string Screen { get; set; }
    }
}
