using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.UserRoles;
using UserApp.Service.Services.UserRoles.Dtos;

namespace UserApp.Api.Controllers.UserRoles
{
    [Route("[controller]")]
    [ApiController]
    public class UserRoleController : Controller
    {
        IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService) {
            _userRoleService = userRoleService;
        }

        [HttpGet("to-edit/{userRoleId}")]
        public IResult GetToEdit(int userRoleId)
        {
            var resume = _userRoleService.GetUserRoleToEdit(userRoleId);
            return Results.Ok(resume);
        }

        [HttpGet("resume")]
        public IResult GetResume()
        {
            var resume = _userRoleService.GetAllResume();
            return Results.Ok(resume);
        }

        [HttpGet("grid")]
        public IResult GetGrid()
        {
            var grid = _userRoleService.GetGrid();
            return Results.Ok(grid);
        }

        [HttpPost()]
        public IResult Create(CreateUserRoleDto createDto)
        {
            var id = _userRoleService.CreateUserRole(createDto);
            return Results.Ok(id);
        }

        [HttpPut()]
        public IResult Update(UpdateUserRoleDto update)
        {
            _userRoleService.Update(update);
            return Results.Ok("Updated");
        }

        [HttpDelete("{userRoleId}")]
        public IResult Delete(int userRoleId)
        {
            _userRoleService.Delete(userRoleId);
            return Results.Ok("Eliminado");
        }
    }
}
