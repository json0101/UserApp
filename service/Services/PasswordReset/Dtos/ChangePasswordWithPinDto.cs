namespace UserApp.Service.Services.PasswordReset.Dtos
{
    public record ChangePasswordWithPinDto
    (
        string employeeCode,
        string pin,
        string password,
        string confirmPassword
    );
}
