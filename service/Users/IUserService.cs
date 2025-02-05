using UserApp.Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Users
{
    public interface IUserService
    {
        UserDto GetUserByEmail(string email);
    }
}
