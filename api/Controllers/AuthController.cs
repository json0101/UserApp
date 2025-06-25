using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.Autentication;

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
        [HttpGet("employeeCod/{employeeCod}")]
        public IResult GetEmployee(string employeeCod)
        {
            try {
                string message = "";
                var userDto = _userAppAuthService.UserValidByEmployeeCod(employeeCod, out message);

                return Results.Ok(userDto);
            } catch (Exception ex) {
                return Results.BadRequest(ex.Message);
            }
            
            
        }
    }
}
