using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Global;
using UserApp.Service.Services.Users;
using UserApp.Service.Services.Users.Dto;
using UserApp.Service.Services.UsersApplications;

namespace UserApp.Service.Services.Autentication
{
    public class UserAppAuthService : IUserAppAuthService
    {
        private readonly IUserService _userService;
        private readonly IUserApplicationService _userApplicationService;
        public UserAppAuthService(IUserService userService, IUserApplicationService userApplicationService)
        {
            _userService = userService;
            _userApplicationService = userApplicationService;
        }
        public UserDto? UserValidByEmployeeCod(string employeeCod, out string message)
        {
            var user = _userService.GetUserByEmployeeCod(employeeCod);

            if (user == null)
            {
                message = "User not found";
                return null;
            }

            var validUserApplication = _userApplicationService.ValidUserApplication(ApplicationGlobal.ApplicationGlobalID, user.Id);

            if (validUserApplication == false)
            {
                message = "User doesn't have permissions in this applications";
                return null;
            }

            message = "Correct";
            return user;
        }
    }
}
