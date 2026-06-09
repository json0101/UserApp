namespace UserApp.Service.Services.UsersLoginLogs.Dtos
{
    public record CreateUserLoginLogDto
    (
        int userId,
        int applicationId,
        bool successful,
        string? ipAddress = null,
        string userAgent = null,
        string? failureReason = null
    );
}
