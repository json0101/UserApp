using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UserApp.Service.Services.Autentication;
using UserApp.Service.Services.Autentication.Dtos;

namespace UserApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        IUserAppAuthService _userAppAuthService;
        public AuthController(IUserAppAuthService userAppAuthService)
        {
            _userAppAuthService = userAppAuthService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [EnableRateLimiting("login")]
        public IResult Login(LoginDto loginDto)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers.UserAgent.ToString();

            var dto = _userAppAuthService.Login(loginDto, ipAddress, userAgent);

            return Results.Ok(dto);
        }
    }
}
