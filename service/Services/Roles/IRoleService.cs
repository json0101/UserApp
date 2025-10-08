using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.Roles.Dtos;

namespace UserApp.Service.Services.Roles
{
    public interface IRoleService
    {
        List<RoleResumeDto> GetAllResume();
        
    }
}
