using System;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.UsersLoginLogs.Dtos;

namespace UserApp.Service.Services.UsersLoginLogs
{
    public class UserLoginLogService : IUserLoginLogService
    {
        private readonly IRepository<UserLoginLog> _repositoryUserLoginLog;
        public UserLoginLogService(IRepository<UserLoginLog> repositoryUserLoginLog)
        {
            _repositoryUserLoginLog = repositoryUserLoginLog;
        }

        public int RegisterLogin(CreateUserLoginLogDto create)
        {
            UserLoginLog userLoginLog = new UserLoginLog();
            userLoginLog.UserId = create.userId;
            userLoginLog.ApplicationId = create.applicationId;
            userLoginLog.Successful = create.successful;
            userLoginLog.IpAddress = create.ipAddress;
            userLoginLog.FailureReason = create.failureReason;
            userLoginLog.CreatedAt = DateTime.Now.ToUniversalTime();
            userLoginLog.CreatedBy = create.userAgent;
            userLoginLog.Active = true;

            _repositoryUserLoginLog.Insert(userLoginLog);
            _repositoryUserLoginLog.SaveChanges();

            return userLoginLog.Id;
        }
    }
}
