using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.Roles.Dtos
{
    public record CreateRoleDto
    (
        int applicationId,
        string description
    );
}
