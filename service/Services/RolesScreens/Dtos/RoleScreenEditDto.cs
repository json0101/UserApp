using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.RolesScreens.Dtos
{
    public record RoleScreenEditDto
    (
        int roleId,
        int screenId
    );
}
