using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Commons;

namespace UserApp.Service.Services.UserRoles.Dtos
{
    public class UserRoleGridDto: BaseDto
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }

        public UserRoleGridDto() { }
    }
}
