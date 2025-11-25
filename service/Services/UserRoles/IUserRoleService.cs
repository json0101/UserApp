using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.UserRoles.Dtos;

namespace UserApp.Service.Services.UserRoles
{
    public interface IUserRoleService
    {
        List<UserRoleResumeDto> GetAllResume();
        List<UserRoleGridDto> GetGrid();
        int CreateUserRole(CreateUserRoleDto create);
        void Delete(int userRoleId);
        UserRoleToEditDto GetUserRoleToEdit(int userRoleId);
        void Update(UpdateUserRoleDto update);
    }
}
