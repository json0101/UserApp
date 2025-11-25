using service.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.UserRoles.Dtos;


namespace UserApp.Service.Services.UserRoles
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepository<UserRole> _userRoleRepository;
        public UserRoleService(IRepository<UserRole> userRoleRepository) {
            _userRoleRepository = userRoleRepository;
        }

        public bool ExistUserRole(CreateUserRoleDto create)
        {
            var userRole = _userRoleRepository
                            .GetDbSet()
                            .Where(x => 
                                    x.UserId == create.userId 
                                    && x.RoleId == create.roleId
                                    && x.Active == true
                            )
                            .FirstOrDefault();

            if (userRole != null)
            {
                throw new BadRequestException("Este usuario ya tiene asignado ese rol");
            }

            return true;
        }
        public int CreateUserRole(CreateUserRoleDto create)
        {
            ExistUserRole(create);

            UserRole userRole = new UserRole();
            userRole.UserId = create.userId;
            userRole.RoleId = create.roleId;
            userRole.CreatedAt = DateTime.Now.ToUniversalTime();
            userRole.CreatedBy = "";
            userRole.Active = true;

            _userRoleRepository.Insert(userRole);
            _userRoleRepository.SaveChanges();

            return userRole.Id;
        }

        public List<UserRoleResumeDto> GetAllResume()
        {
            var userRoleResume = (from r in _userRoleRepository.GetDbSet()
                             where r.Active
                             select new UserRoleResumeDto(r.Id, r.User.UserName, r.Role.Description)
                             ).ToList();

            return userRoleResume;
        }

        public List<UserRoleGridDto> GetGrid()
        {
            var userRoleGrid = (from userR in _userRoleRepository.GetDbSet()
                             where userR.Active
                             select new UserRoleGridDto()
                                 {
                                     Id = userR.Id,
                                     UserId = userR.UserId,
                                     User = userR.User.UserName,
                                     Role = userR.Role.Description,
                                     RoleId = userR.RoleId,
                                     CreatedAt = userR.CreatedAt,
                                     CreatedBy = userR.CreatedBy,
                                     UpdatedAt = userR.UpdatedAt,
                                     UpdatedBy = userR.UpdatedBy,
                                     Active = userR.Active,
                                 }       
                             ).ToList();

            return userRoleGrid;
        }

        public void Delete(int userRoleId)
        {
            var userRole = _userRoleRepository.GetDbSet().Where(userR => userR.Id == userRoleId).FirstOrDefault();

            if (userRole == null) {
                throw new Exception("El userRole no se encontro");
            }

            userRole.UpdatedAt = DateTime.Now.ToUniversalTime();
            userRole.UpdatedBy = "";
            userRole.Active = false;
            _userRoleRepository.SaveChanges();
        }

        public UserRoleToEditDto GetUserRoleToEdit(int userRoleId)
        {
            var userRole = (from userR in _userRoleRepository.GetDbSet()
                            where userR.Id == userRoleId
                            select new UserRoleToEditDto(userR.Id, userR.UserId, userR.RoleId)
                        )
                        .FirstOrDefault();

            if (userRole == null)
            {
                throw new Exception("No se encontro el userRole");
            }

            return userRole;
        }

        public void Update(UpdateUserRoleDto update)
        {
            ExistUserRole(update);
            var userRole = _userRoleRepository.GetDbSet().Where(userR => userR.Id == update.userRoleId).FirstOrDefault();

            if (userRole == null)
            {
                throw new Exception("No se encontro el userRole");
            }

            userRole.UserId = update.userId;
            userRole.RoleId = update.roleId;

            userRole.UpdatedAt = DateTime.Now.ToUniversalTime();
            userRole.UpdatedBy = "";
            
            _userRoleRepository.SaveChanges();
        }
    }
}
