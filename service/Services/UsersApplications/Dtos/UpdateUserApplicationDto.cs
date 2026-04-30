namespace UserApp.Service.Services.UsersApplications.Dtos
{
    public record UpdateUserApplicationDto(int userApplicationId, int userId, int applicationId): CreateUserApplicationDto(userId, applicationId);
}
