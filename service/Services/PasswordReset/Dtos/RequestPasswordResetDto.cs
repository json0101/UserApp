namespace UserApp.Service.Services.PasswordReset.Dtos
{
    public record RequestPasswordResetDto
    (
        string employeeCode,
        string email
    );
}
