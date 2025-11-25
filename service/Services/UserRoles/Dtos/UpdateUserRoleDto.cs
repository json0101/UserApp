using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.UserRoles.Dtos
{
    public record UpdateUserRoleDto(int userRoleId, int userId, int roleId): CreateUserRoleDto(userId, roleId);
    
}
