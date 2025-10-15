using service.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.RolesScreens.Dtos;

namespace UserApp.Service.Services.RolesScreens
{
    public class RoleScreenService : IRoleScreenService
    {
        private readonly IRepository<RoleScreen> _repositoryRoleScreen;
        public RoleScreenService(IRepository<RoleScreen> repositoryRoleScreen) {
            _repositoryRoleScreen = repositoryRoleScreen;
        }
        public List<RoleScreenGridDto> GetGrid()
        {
            var roleScreen = (from rs in _repositoryRoleScreen.GetDbSet()
                              where rs.Active == true
                                  select new RoleScreenGridDto()
                                  {
                                      Id = rs.Id,
                                      RoleId = rs.RoleId,
                                      Role = rs.Role.Description,
                                      ScreenId = rs.ScreenId,
                                      Screen = rs.Screen.Name,
                                      CreatedAt = rs.CreatedAt,
                                      CreatedBy = rs.CreatedBy,
                                      UpdatedAt = rs.UpdatedAt, 
                                      UpdatedBy = rs.UpdatedBy,
                                      Active = rs.Active
                                  }

                              ).ToList();

            return roleScreen;
        }

        public int Create(CreateRoleScreenDto createRole)
        {
            if(createRole.roleId == 0)
            {
                throw new BadRequestException("Select the role");
            }

            if(createRole.screenId == 0)
            {
                throw new BadRequestException("Select the screen");
            }

            var rolescreen = new RoleScreen();
            rolescreen.RoleId = createRole.roleId;
            rolescreen.ScreenId = createRole.screenId;

            rolescreen.CreatedAt = DateTime.Now.ToUniversalTime();
            rolescreen.CreatedBy = "jason.hernandez";
            rolescreen.Active = true;

            _repositoryRoleScreen.Insert(rolescreen);
            _repositoryRoleScreen.SaveChanges();

            return rolescreen.Id;
        }

        public void Delete(int roleScreenId)
        {
            var roleScreen = _repositoryRoleScreen.GetDbSet().Where(x => x.Id == roleScreenId).FirstOrDefault();

            if(roleScreen == null)
            {
                throw new BadRequestException("Role Screen doesn't exists");
            }

            roleScreen.Active = false;
            roleScreen.UpdatedAt = DateTime.Now.ToUniversalTime();
            roleScreen.UpdatedBy = "";

            _repositoryRoleScreen.SaveChanges();
        }

        public RoleScreenEditDto Get(int roleScreenId)
        {
            var roleScreen = (
                    from rs in _repositoryRoleScreen.GetDbSet()
                    where rs.Id == roleScreenId
                    select new RoleScreenEditDto(rs.Id, rs.RoleId, rs.ScreenId)                    
            ).FirstOrDefault();

            if (roleScreen == null)
            {
                throw new BadRequestException("Role Screen doesn't exists");
            }

            return roleScreen;
        }

        public void Update(UpdateRoleScreenDto updateRole)
        {
            var update = _repositoryRoleScreen
                            .GetDbSet()
                            .Where(x => x.Id == updateRole.id && x.Active)
                            .FirstOrDefault();


            if (update == null)
            {
                throw new BadRequestException("Role Screen doesn't exists");
            }

            update.ScreenId = updateRole.screenId;
            update.RoleId = updateRole.roleId;

            update.UpdatedBy = "jason.hernandez";
            update.UpdatedAt = DateTime.Now.ToUniversalTime();

            _repositoryRoleScreen.SaveChanges();
        }
    }
}
