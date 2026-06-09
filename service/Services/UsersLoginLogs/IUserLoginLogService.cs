using UserApp.Service.Services.UsersLoginLogs.Dtos;

namespace UserApp.Service.Services.UsersLoginLogs
{
    public interface IUserLoginLogService
    {
        long RegisterLogin(CreateUserLoginLogDto create);
    }
}
