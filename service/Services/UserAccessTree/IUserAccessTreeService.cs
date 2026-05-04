using UserApp.Service.Services.UserAccessTree.Dtos;

namespace UserApp.Service.Services.UserAccessTree
{
    public interface IUserAccessTreeService
    {
        List<UserAccessTreeApplicationDto> GetByUser(int userId);
    }
}
