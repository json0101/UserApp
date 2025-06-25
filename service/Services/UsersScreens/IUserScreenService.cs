using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.UsersScreens.Dtos;

namespace UserApp.Service.Services.UsersScreens
{
    public interface IUserScreenService
    {
        List<MenuDto> GetMenu(int userId);
    }
}
