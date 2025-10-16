using UserApp.Domain;
using UserApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UserApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Global;
using UserApp.Service.Services.UsersScreens.Dtos;

namespace UserApp.Service.Services.UsersScreens
{
    public class UserScreenService : IUserScreenService
    {
        public IRepository<User> _userReporsitory { get; set; }
        public IRepository<Role> _roleReporsitory { get; set; }
        public IRepository<Screen> _screenReporsitory { get; set; }
        UserAppContext _context;

        public UserScreenService(IRepository<User> userReporsitory, IRepository<Screen> screenReporsitory, IRepository<Role> roleReporsitory, UserAppContext context)
        {
            _userReporsitory = userReporsitory;
            _screenReporsitory = screenReporsitory;
            _roleReporsitory = roleReporsitory;
            _context = context;
        }
        public List<MenuDto> GetMenu(int userId)
        {
            var user_screen_query =
                    from u in _context.User
                    join ur in _context.UserRole on u.Id equals ur.UserId
                    join r in _context.Role on ur.RoleId equals r.Id
                    join rs in _context.RoleScreen on r.Id equals rs.RoleId
                    join s in _context.Screen on rs.ScreenId equals s.Id
                    where u.Id == userId && s.ApplicationId == ApplicationGlobal.ApplicationGlobalID
                    select new MenuDto
                    {
                        ScreenId = s.Id,
                        Name = s.Name,
                        Route = s.Route,
                        IsFather = s.IsFather,
                        ScreenFatherId = s.ScreenFatherId,
                        Order = s.Order,
                        Children = new List<MenuDto>()
                    }
            ;

            var sql = user_screen_query.ToQueryString();
             var list = user_screen_query.ToList();

            return list;
        }
    }
}
