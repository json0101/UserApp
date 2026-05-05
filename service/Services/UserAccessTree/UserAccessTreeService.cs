using Microsoft.EntityFrameworkCore;
using UserApp.Domain;
using UserApp.Service.Services.UserAccessTree.Dtos;

namespace UserApp.Service.Services.UserAccessTree
{
    public class UserAccessTreeService : IUserAccessTreeService
    {
        private readonly UserAppContext _context;

        public UserAccessTreeService(UserAppContext context)
        {
            _context = context;
        }

        public List<UserAccessTreeApplicationDto> GetByUser(int userId)
        {
            var applications = (
                from application in _context.Application.AsNoTracking()
                where application.Active
                orderby application.Description
                select new UserAccessTreeApplicationDto
                {
                    Id = application.Id,
                    Description = application.Description,
                }
            ).ToList();

            var userApplicationIds = _context.UserApplication.AsNoTracking()
                .Where(userApplication => userApplication.UserId == userId && userApplication.Active)
                .Select(userApplication => userApplication.ApplicationId)
                .ToHashSet();

            var userScreenIds = (
                from userRole in _context.UserRole.AsNoTracking()
                join role in _context.Role.AsNoTracking()
                    on userRole.RoleId equals role.Id
                join roleScreen in _context.RoleScreen.AsNoTracking()
                    on role.Id equals roleScreen.RoleId
                join screen in _context.Screen.AsNoTracking()
                    on roleScreen.ScreenId equals screen.Id
                join userApplication in _context.UserApplication.AsNoTracking()
                    on new { UserId = userRole.UserId, ApplicationId = screen.ApplicationId ?? 0 }
                    equals new { userApplication.UserId, userApplication.ApplicationId }
                where userRole.UserId == userId
                    && userRole.Active
                    && role.Active
                    && roleScreen.Active
                    && screen.Active
                    && userApplication.Active
                select screen.Id
            )
            .Distinct()
            .ToHashSet();

            var screens = (
                from screen in _context.Screen.AsNoTracking()
                join application in _context.Application.AsNoTracking()
                    on screen.ApplicationId equals (int?)application.Id
                where screen.Active
                    && application.Active
                select new
                {
                    screen.Id,
                    screen.Name,
                    screen.Route,
                    screen.Order,
                    screen.ScreenFatherId,
                    screen.IsFather,
                    ApplicationId = screen.ApplicationId ?? 0,
                }
            )
            .OrderBy(screen => screen.ApplicationId)
            .ThenBy(screen => screen.Order)
            .ThenBy(screen => screen.Name)
            .ToList();

            var screenIds = screens.Select(screen => screen.Id).ToList();

            var actions = (
                from screenAction in _context.ScreenAction.AsNoTracking()
                join action in _context.Action.AsNoTracking()
                    on screenAction.ActionId equals action.Id
                where screenIds.Contains(screenAction.ScreenId)
                    && screenAction.Active
                    && action.Active
                orderby action.Description
                select new
                {
                    screenAction.ScreenId,
                    ActionId = action.Id,
                    action.Description,
                }
            )
            .Distinct()
            .ToList();

            foreach (var application in applications)
            {
                application.Screens = screens
                    .Where(screen => screen.ApplicationId == application.Id)
                    .Select(screen => new UserAccessTreeScreenDto
                    {
                        Id = screen.Id,
                        Name = screen.Name,
                        Route = screen.Route,
                        Order = screen.Order,
                        ScreenFatherId = screen.ScreenFatherId,
                        IsFather = screen.IsFather,
                        HasAccess = userScreenIds.Contains(screen.Id),
                        Actions = actions
                            .Where(action => action.ScreenId == screen.Id)
                            .Select(action => new UserAccessTreeActionDto
                            {
                                Id = action.ActionId,
                                Description = action.Description,
                                HasAccess = userScreenIds.Contains(screen.Id),
                            })
                            .ToList()
                    })
                    .ToList();

                application.HasAccess = userApplicationIds.Contains(application.Id);
            }

            return applications;
        }
    }
}
