using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.RolesScreens.Dtos;

namespace UserApp.Service.Services.RolesScreens
{
    public interface IRoleScreenService
    {
        List<RoleScreenGridDto> GetGrid();
        int Get(int roleScreenId);
        int Create(CreateRoleScreenDto createRole);
        void Delete(int roleScreenId);
    }
}
