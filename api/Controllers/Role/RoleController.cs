using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.Roles;
using UserApp.Service.Services.Roles.Dtos;

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

        [HttpGet("to-edit/{roleId}")]
        public IResult GetToEdit(int roleId)
        {
            var resume = _rolService.GetRoleToEdit(roleId);
            return Results.Ok(resume);
        }

        [HttpGet("resume")]
        public IResult GetResume()
        {
            var resume = _rolService.GetAllResume();
            return Results.Ok(resume);
        }

        [HttpGet("grid")]
        public IResult GetGrid()
        {
            var grid = _rolService.GetGrid();
            return Results.Ok(grid);
        }

        [HttpPost()]
        public IResult Create(CreateRoleDto createDto)
        {
            var id = _rolService.CreateRole(createDto);
            return Results.Ok(id);
        }

        [HttpPut()]
        public IResult Update(UpdateRoleDto update)
        {
            _rolService.Update(update);
            return Results.Ok("Updated");
        }

        [HttpDelete("{roleId}")]
        public IResult Delete(int roleId)
        {
            _rolService.Delete(roleId);
            return Results.Ok("Eliminado");
        }
    }
}
