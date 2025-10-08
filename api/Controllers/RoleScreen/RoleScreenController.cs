using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.RolesScreens;
using UserApp.Service.Services.RolesScreens.Dtos;

namespace UserApp.Api.Controllers.RoleScreen
{
    [Route("[controller]")]
    [ApiController]
    public class RoleScreenController : Controller
    {
        IRoleScreenService _roleScreenService;
        public RoleScreenController(IRoleScreenService roleScreenService) { 
            _roleScreenService = roleScreenService;
        }

        [HttpGet("{roleScreenId}")]
        public IResult Get(int roleScreenId)
        {
            var rs = _roleScreenService.Get(roleScreenId);
            return Results.Ok(rs);
        }

        [HttpGet("grid")]
        public IResult GetGrid()
        {
            var rs = _roleScreenService.GetGrid();
            return Results.Ok(rs);
        }

        [HttpPost()]
        public IResult Create(CreateRoleScreenDto create)
        {
            var rs = _roleScreenService.Create(create);
            return Results.Ok(rs);
        }

        [HttpDelete("{roleScreenId}")]
        public IResult Delete(int roleScreenId)
        {
            _roleScreenService.Delete(roleScreenId);
            return Results.Ok("OK");
        }
    }
}
