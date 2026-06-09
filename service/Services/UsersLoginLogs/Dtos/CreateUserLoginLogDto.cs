namespace UserApp.Service.Services.UsersLoginLogs.Dtos
{
    public record CreateUserLoginLogDto
    (
        int? userId,
        int applicationId,
        string? userName,
        bool successful,
        string? ipAddress = null,
        string? userAgent = null,
        string? failureReason = null
    );
}
