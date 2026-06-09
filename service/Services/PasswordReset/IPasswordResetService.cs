using UserApp.Service.Services.PasswordReset.Dtos;

namespace UserApp.Service.Services.PasswordReset
{
    public interface IPasswordResetService
    {
        string RequestPasswordReset(RequestPasswordResetDto dto);
        void ValidatePin(ValidatePinDto dto);
        string ChangePassword(ChangePasswordWithPinDto dto);
    }
}
