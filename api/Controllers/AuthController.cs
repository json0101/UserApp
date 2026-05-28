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
            var dto = _userAppAuthService.Login(loginDto);

            return Results.Ok(dto);
        }
    }
}
