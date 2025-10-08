using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.Roles;

namespace UserApp.Api.Controllers.Role
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        IRoleService _rolService;
        public RoleController(IRoleService rolService) {
            _rolService = rolService;
        }

        [HttpGet("resume")]
        public IResult GetResume()
        {
            var resume = _rolService.GetAllResume();
            return Results.Ok(resume);
        }
    }
}
