using UserApp.Service.Services.UsersLoginLogs.Dtos;

namespace UserApp.Service.Services.UsersLoginLogs
{
    public interface IUserLoginLogService
    {
        int RegisterLogin(CreateUserLoginLogDto create);
    }
}
