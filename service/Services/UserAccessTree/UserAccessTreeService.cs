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

        public List<UserAccessTreeApplicationDto> GetByUser(int userId, int? roleId = null, int? applicationId = null)
        {
            var applications = GetActiveApplications();

            var userApplicationIds = _context.UserApplication.AsNoTracking()
                .Where(userApplication => userApplication.UserId == userId && userApplication.Active)
                .Select(userApplication => userApplication.ApplicationId)
                .ToHashSet();

            var userScreenAccess = (
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
                    && (!roleId.HasValue || userRole.RoleId == roleId.Value)
                    && (!applicationId.HasValue || role.ApplicationId == applicationId.Value)
                select new
                {
                    ScreenId = screen.Id,
                    RoleScreenId = roleScreen.Id,
                }
            )
            .Distinct()
            .ToList();

            var userScreenIds = userScreenAccess
                .Select(screenAccess => screenAccess.ScreenId)
                .ToHashSet();

            var roleScreenIdsByScreen = userScreenAccess
                .GroupBy(screenAccess => screenAccess.ScreenId)
                .ToDictionary(
                    screenAccess => screenAccess.Key,
                    screenAccess => screenAccess.First().RoleScreenId
                );

            var screens = GetActiveScreens();
            var actions = GetActiveActions(screens.Select(screen => screen.Id).ToList());

            foreach (var application in applications)
            {
                application.Screens = BuildScreens(
                    application.Id,
                    screens,
                    actions,
                    screenId => userScreenIds.Contains(screenId),
                    screenId => roleScreenIdsByScreen.TryGetValue(screenId, out var roleScreenId)
                        ? roleScreenId
                        : null
                );

                application.HasAccess = userApplicationIds.Contains(application.Id);
            }

            return applications;
        }

        public UserAccessTreeApplicationDto GetByApplication(int applicationId)
        {
            var application = GetActiveApplications(applicationId).FirstOrDefault();

            if (application == null)
            {
                throw new Exception("La aplicacion no se encontro.");
            }

            var screens = GetActiveScreens(applicationId);
            var actions = GetActiveActions(screens.Select(screen => screen.Id).ToList());

            application.HasAccess = true;
            application.Screens = BuildScreens(
                application.Id,
                screens,
                actions,
                _ => true,
                _ => null
            );

            return application;
        }

        private List<UserAccessTreeApplicationDto> GetActiveApplications(int? applicationId = null)
        {
            return (
                from application in _context.Application.AsNoTracking()
                where application.Active
                    && (!applicationId.HasValue || application.Id == applicationId.Value)
                orderby application.Description
                select new UserAccessTreeApplicationDto
                {
                    Id = application.Id,
                    Description = application.Description,
                }
            ).ToList();
        }

        private List<ScreenTreeData> GetActiveScreens(int? applicationId = null)
        {
            return (
                from screen in _context.Screen.AsNoTracking()
                join application in _context.Application.AsNoTracking()
                    on screen.ApplicationId equals (int?)application.Id
                where screen.Active
                    && application.Active
                    && (!applicationId.HasValue || screen.ApplicationId == applicationId.Value)
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
            .Select(screen => new ScreenTreeData
            {
                Id = screen.Id,
                Name = screen.Name,
                Route = screen.Route,
                Order = screen.Order,
                ScreenFatherId = screen.ScreenFatherId,
                IsFather = screen.IsFather,
                ApplicationId = screen.ApplicationId,
            })
            .ToList();
        }

        private List<ActionTreeData> GetActiveActions(List<int> screenIds)
        {
            if (screenIds.Count == 0)
            {
                return new List<ActionTreeData>();
            }

            return (
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
            .Select(action => new ActionTreeData
            {
                ScreenId = action.ScreenId,
                ActionId = action.ActionId,
                Description = action.Description,
            })
            .ToList();
        }

        private static List<UserAccessTreeScreenDto> BuildScreens(
            int applicationId,
            List<ScreenTreeData> screens,
            List<ActionTreeData> actions,
            Func<int, bool> hasScreenAccess,
            Func<int, int?> getRoleScreenId
        )
        {
            return screens
                .Where(screen => screen.ApplicationId == applicationId)
                .Select(screen => new UserAccessTreeScreenDto
                {
                    Id = screen.Id,
                    Name = screen.Name,
                    Route = screen.Route,
                    Order = screen.Order,
                    ScreenFatherId = screen.ScreenFatherId,
                    IsFather = screen.IsFather,
                    HasAccess = hasScreenAccess(screen.Id),
                    RoleScreenId = getRoleScreenId(screen.Id),
                    Actions = actions
                        .Where(action => action.ScreenId == screen.Id)
                        .Select(action => new UserAccessTreeActionDto
                        {
                            Id = action.ActionId,
                            Description = action.Description,
                            HasAccess = hasScreenAccess(screen.Id),
                        })
                        .ToList()
                })
                .ToList();
        }

        private class ScreenTreeData
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Route { get; set; } = string.Empty;
            public int Order { get; set; }
            public int? ScreenFatherId { get; set; }
            public bool IsFather { get; set; }
            public int ApplicationId { get; set; }
        }

        private class ActionTreeData
        {
            public int ScreenId { get; set; }
            public int ActionId { get; set; }
            public string Description { get; set; } = string.Empty;
        }
    }
}
