using Microsoft.AspNetCore.Mvc;
using UserApp.Service.UsersScreens;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserScreenController : Controller
    {
        IUserScreenService _userScreenService;
        public UserScreenController(IUserScreenService userScreenService) {
            _userScreenService = userScreenService;
        }

        [HttpGet("{userid}")]
        public IResult Get(int userid)
        {
            return Results.Ok(_userScreenService.GetMenu(userid));
        }
    }
}
