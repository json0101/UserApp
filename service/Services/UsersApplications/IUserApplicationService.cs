using UserApp.Service.Services.UsersApplications.Dtos;

namespace UserApp.Service.Services.UsersApplications
{
    public interface IUserApplicationService
    {
        bool ValidUserApplication(int application_id, int user_id);
        List<UserApplicationResumeDto> GetAllResume();
        List<UserApplicationGridDto> GetGrid();
        int CreateUserApplication(CreateUserApplicationDto create);
        void Delete(int userApplicationId);
        UserApplicationToEditDto GetUserApplicationToEdit(int userApplicationId);
        void Update(UpdateUserApplicationDto update);
    }
}
