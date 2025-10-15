using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.Users.Dto
{
    public record CreateUserDto(string username, string email, string employeeCode, string password, string confirmPassword);
}
