using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Commons;

namespace UserApp.Service.Services.Users.Dto
{
    public class UserGridDto: BaseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmployeeCode { get; set; }
    }
}
