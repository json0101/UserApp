using service.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Commons.CurrentUser;
using UserApp.Service.Services.RolesScreens.Dtos;

namespace UserApp.Service.Services.RolesScreens
{
    public class RoleScreenService : IRoleScreenService
    {
        private readonly IRepository<RoleScreen> _repositoryRoleScreen;
        private readonly IRepository<Screen> _repositoryScreen;
        private readonly ICurrentUserService _currentUser;

        public RoleScreenService(IRepository<RoleScreen> repositoryRoleScreen, IRepository<Screen> repositoryScreen, ICurrentUserService currentUser) {
            _repositoryRoleScreen = repositoryRoleScreen;
            _repositoryScreen = repositoryScreen;
            _currentUser = currentUser;
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
                                      Application = rs.Screen.Application.Description,
                                      CreatedAt = rs.CreatedAt,
                                      CreatedBy = rs.CreatedBy,
                                      UpdatedAt = rs.UpdatedAt, 
                                      UpdatedBy = rs.UpdatedBy,
                                      Active = rs.Active
                                  }
                              ).ToList();

            return roleScreen;
        }

        public List<RoleScreenAccessChangeDto> Create(CreateRoleScreenDto createRole)
        {
            if(createRole.roleId == 0)
            {
                throw new BadRequestException("Select the role");
            }

            if(createRole.screenId == 0)
            {
                throw new BadRequestException("Select the screen");
            }

            var rolescreen = _repositoryRoleScreen.GetDbSet()
                .Where(roleScreen => roleScreen.RoleId == createRole.roleId
                    && roleScreen.ScreenId == createRole.screenId
                    && roleScreen.Active)
                .FirstOrDefault();

            if (rolescreen == null)
            {
                rolescreen = CreateRoleScreen(createRole.roleId, createRole.screenId);
            }

            var changes = new List<RoleScreenAccessChangeDto>
            {
                new RoleScreenAccessChangeDto
                {
                    ScreenId = rolescreen.ScreenId,
                    RoleScreenId = rolescreen.Id,
                    HasAccess = true,
                }
            };

            changes.AddRange(AddMissingScreenFathers(createRole.roleId, createRole.screenId));

            _repositoryRoleScreen.SaveChanges();

            changes.ForEach(change =>
            {
                if (change.RoleScreenId == 0)
                {
                    change.RoleScreenId = _repositoryRoleScreen.GetDbSet()
                        .Where(roleScreen => roleScreen.RoleId == createRole.roleId
                            && roleScreen.ScreenId == change.ScreenId
                            && roleScreen.Active)
                        .Select(roleScreen => roleScreen.Id)
                        .FirstOrDefault();
                }
            });

            return changes;
        }

        public List<RoleScreenAccessChangeDto> Delete(int roleScreenId)
        {
            var roleScreen = _repositoryRoleScreen.GetDbSet()
                .Where(x => x.Id == roleScreenId && x.Active)
                .FirstOrDefault();

            if(roleScreen == null)
            {
                throw new BadRequestException("Role Screen doesn't exists");
            }

            var changes = new List<RoleScreenAccessChangeDto>
            {
                new RoleScreenAccessChangeDto
                {
                    ScreenId = roleScreen.ScreenId,
                    RoleScreenId = roleScreen.Id,
                    HasAccess = false,
                }
            };

            DeactivateRoleScreen(roleScreen);
            _repositoryRoleScreen.SaveChanges();

            changes.AddRange(RemoveEmptyScreenFathers(roleScreen.RoleId, roleScreen.ScreenId));

            _repositoryRoleScreen.SaveChanges();

            return changes;
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

            update.UpdatedBy = _currentUser.UserName;
            update.UpdatedAt = DateTime.Now.ToUniversalTime();

            _repositoryRoleScreen.SaveChanges();
        }

        private RoleScreen CreateRoleScreen(int roleId, int screenId)
        {
            var rolescreen = new RoleScreen
            {
                RoleId = roleId,
                ScreenId = screenId,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                CreatedBy = _currentUser.UserName,
                Active = true,
            };

            _repositoryRoleScreen.GetDbSet().Add(rolescreen);

            return rolescreen;
        }

        private List<RoleScreenAccessChangeDto> AddMissingScreenFathers(int roleId, int screenId)
        {
            var changes = new List<RoleScreenAccessChangeDto>();
            var screenFatherId = _repositoryScreen.GetDbSet()
                .Where(screen => screen.Id == screenId && screen.Active)
                .Select(screen => screen.ScreenFatherId)
                .FirstOrDefault();

            while (screenFatherId.HasValue)
            {
                var fatherRoleScreenExists = _repositoryRoleScreen.GetDbSet()
                    .Any(roleScreen => roleScreen.RoleId == roleId
                        && roleScreen.ScreenId == screenFatherId.Value
                        && roleScreen.Active);

                if (!fatherRoleScreenExists)
                {
                    var fatherRoleScreen = CreateRoleScreen(roleId, screenFatherId.Value);
                    changes.Add(new RoleScreenAccessChangeDto
                    {
                        ScreenId = fatherRoleScreen.ScreenId,
                        RoleScreenId = fatherRoleScreen.Id,
                        HasAccess = true,
                    });
                }

                screenFatherId = _repositoryScreen.GetDbSet()
                    .Where(screen => screen.Id == screenFatherId.Value && screen.Active)
                    .Select(screen => screen.ScreenFatherId)
                    .FirstOrDefault();
            }

            return changes;
        }

        private List<RoleScreenAccessChangeDto> RemoveEmptyScreenFathers(int roleId, int screenId)
        {
            var changes = new List<RoleScreenAccessChangeDto>();
            var screenFatherId = _repositoryScreen.GetDbSet()
                .Where(screen => screen.Id == screenId && screen.Active)
                .Select(screen => screen.ScreenFatherId)
                .FirstOrDefault();

            while (screenFatherId.HasValue)
            {
                var fatherHasActiveChildren = (
                    from childScreen in _repositoryScreen.GetDbSet()
                    join roleScreen in _repositoryRoleScreen.GetDbSet()
                        on childScreen.Id equals roleScreen.ScreenId
                    where childScreen.ScreenFatherId == screenFatherId.Value
                        && childScreen.Active
                        && roleScreen.RoleId == roleId
                        && roleScreen.Active
                    select childScreen.Id
                ).Any();

                if (fatherHasActiveChildren)
                {
                    return changes;
                }

                var fatherRoleScreen = _repositoryRoleScreen.GetDbSet()
                    .Where(roleScreen => roleScreen.RoleId == roleId
                        && roleScreen.ScreenId == screenFatherId.Value
                        && roleScreen.Active)
                    .FirstOrDefault();

                if (fatherRoleScreen != null)
                {
                    DeactivateRoleScreen(fatherRoleScreen);
                    changes.Add(new RoleScreenAccessChangeDto
                    {
                        ScreenId = fatherRoleScreen.ScreenId,
                        RoleScreenId = fatherRoleScreen.Id,
                        HasAccess = false,
                    });
                    _repositoryRoleScreen.SaveChanges();
                }

                screenFatherId = _repositoryScreen.GetDbSet()
                    .Where(screen => screen.Id == screenFatherId.Value && screen.Active)
                    .Select(screen => screen.ScreenFatherId)
                    .FirstOrDefault();
            }

            return changes;
        }

        private void DeactivateRoleScreen(RoleScreen roleScreen)
        {
            roleScreen.Active = false;
            roleScreen.UpdatedAt = DateTime.Now.ToUniversalTime();
            roleScreen.UpdatedBy = _currentUser.UserName;
        }
    }
}
