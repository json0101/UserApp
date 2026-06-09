using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.PasswordReset;
using UserApp.Service.Services.PasswordReset.Dtos;

namespace UserApp.Api.Controllers
{
    [ApiController]
    [Route("password-reset")]
    public class PasswordResetController : Controller
    {
        private readonly IPasswordResetService _passwordResetService;
        public PasswordResetController(IPasswordResetService passwordResetService)
        {
            _passwordResetService = passwordResetService;
        }

        [AllowAnonymous]
        [HttpPost("request")]
        public IResult Request(RequestPasswordResetDto dto)
        {
            var message = _passwordResetService.RequestPasswordReset(dto);
            return Results.Ok(message);
        }

        [AllowAnonymous]
        [HttpPost("validate-pin")]
        public IResult ValidatePin(ValidatePinDto dto)
        {
            _passwordResetService.ValidatePin(dto);
            return Results.Ok("PIN valido");
        }

        [AllowAnonymous]
        [HttpPost("change")]
        public IResult Change(ChangePasswordWithPinDto dto)
        {
            var message = _passwordResetService.ChangePassword(dto);
            return Results.Ok(message);
        }
    }
}
