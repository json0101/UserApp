using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.UserRoles.Dtos
{
    public record CreateUserRoleDto
    (
        int userId,
        int roleId
    );
}
