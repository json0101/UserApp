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
        public List<RoleResumeDto> GetAllResume()
        {
            var rolResume = (from r in _roleRepository.GetDbSet()
                             where r.Active
                             select new RoleResumeDto(r.Id, r.Description)
                             ).ToList();

            return rolResume;
        }
    }
}
