using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.Roles.Dtos;
using UserApp.Service.Services.RolesScreens.Dtos;

namespace UserApp.Service.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        public RoleService(IRepository<Role> roleRepository) {
            _roleRepository = roleRepository;
        }

        public int CreateRole(CreateRoleDto create)
        {
            Role role = new Role();
            role.Description = create.description;
            role.ApplicationId = create.applicationId;
            role.CreatedAt = DateTime.Now.ToUniversalTime();
            role.CreatedBy = "";
            role.Active = true;

            _roleRepository.Insert(role);
            _roleRepository.SaveChanges();

            return role.Id;
        }

        public List<RoleResumeDto> GetAllResume()
        {
            var rolResume = (from r in _roleRepository.GetDbSet()
                             where r.Active
                             select new RoleResumeDto(r.Id, r.Description)
                             ).ToList();

            return rolResume;
        }

        public List<RoleGridDto> GetGrid()
        {
            var roleGrid = (from r in _roleRepository.GetDbSet()
                             where r.Active
                             select new RoleGridDto()
                                 {
                                     Id = r.Id,
                                     Description = r.Description,
                                     Application = r.Application.Description,
                                     CreatedAt = r.CreatedAt,
                                     CreatedBy = r.CreatedBy,
                                     UpdatedAt = r.UpdatedAt,
                                     UpdatedBy = r.UpdatedBy,
                                     Active = r.Active,
                                 }       
                             ).ToList();

            return roleGrid;
        }

        public void Delete(int roleId)
        {
            var role = _roleRepository.GetDbSet().Where(r => r.Id == roleId).FirstOrDefault();

            if (role == null) {
                throw new Exception("El Rol no se encontro");
            }

            role.UpdatedAt = DateTime.Now.ToUniversalTime();
            role.UpdatedBy = "";
            role.Active = false;
            _roleRepository.SaveChanges();
        }

        public RoleToEditDto GetRoleToEdit(int roleId)
        {
            var role = (from r in _roleRepository.GetDbSet()
                            where r.Id == roleId
                            select new RoleToEditDto(r.Id, r.ApplicationId, r.Description)
                        )
                        .FirstOrDefault();

            if (role == null)
            {
                throw new Exception("No se encontro el role");
            }

            return role;
        }

        public void Update(UpdateRoleDto update)
        {
            var role = _roleRepository.GetDbSet().Where(r => r.Id == update.roleId).FirstOrDefault();

            if (role == null)
            {
                throw new Exception("No se encontro el role");
            }

            role.ApplicationId = update.applicationId;
            role.Description = update.description;

            role.UpdatedAt = DateTime.Now.ToUniversalTime();
            role.UpdatedBy = "";
            
            _roleRepository.SaveChanges();
        }
    }
}
