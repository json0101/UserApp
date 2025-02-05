using UserApp.Service.UsersScreens.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.UsersScreens
{
    public interface IUserScreenService
    {
        List<MenuDto> GetMenu(int userId);
    }
}
