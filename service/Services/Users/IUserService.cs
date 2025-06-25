using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.Users.Dto;

namespace UserApp.Service.Services.Users
{
    public interface IUserService
    {
        UserDto? GetUserByEmail(string email);
        UserDto? GetUserByEmployeeCod(string employeeCod);
    }
}
