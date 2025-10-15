using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.RolesScreens.Dtos
{
    public record UpdateRoleScreenDto(int id, int roleId, int screenId) : CreateRoleScreenDto(roleId, screenId);
    
}
