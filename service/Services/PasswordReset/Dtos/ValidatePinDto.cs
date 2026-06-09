namespace UserApp.Service.Services.PasswordReset.Dtos
{
    public record ValidatePinDto
    (
        string employeeCode,
        string pin
    );
}
