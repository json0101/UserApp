using System;
using UserApp.Domain;
using UserApp.Domain.Entities;
using UserApp.Service.Services.UsersLoginLogs.Dtos;

namespace UserApp.Service.Services.UsersLoginLogs
{
    public class UserLoginLogService : IUserLoginLogService
    {
        private readonly UserAppContext _context;
        public UserLoginLogService(UserAppContext context)
        {
            _context = context;
        }

        public long RegisterLogin(CreateUserLoginLogDto create)
        {
            UserLoginLog userLoginLog = new UserLoginLog();
            userLoginLog.UserId = create.userId;
            userLoginLog.ApplicationId = create.applicationId;
            userLoginLog.UserName = create.userName;
            userLoginLog.Successful = create.successful;
            userLoginLog.IpAddress = create.ipAddress;
            userLoginLog.UserAgent = create.userAgent;
            userLoginLog.FailureReason = create.failureReason;
            userLoginLog.LoginAt = DateTime.Now.ToUniversalTime();

            _context.UserLoginLog.Add(userLoginLog);
            _context.SaveChanges();

            return userLoginLog.Id;
        }
    }
}
