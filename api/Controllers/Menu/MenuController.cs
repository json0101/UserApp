using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserApp.Service.Global;
using UserApp.Service.Services.UsersScreens;

namespace UserApp.Api.Controllers.Menu
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        IUserScreenService _userScreenService;
        public MenuController(IUserScreenService userScreenService)
        {
            _userScreenService = userScreenService;
        }

        [HttpGet]
        public IResult GetMenu()
        {
            var menu = _userScreenService.GetMenu(28);
            return Results.Ok(menu);
        }
    }
}
