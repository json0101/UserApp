using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.UsersApplications;
using UserApp.Service.Services.UsersApplications.Dtos;

namespace UserApp.Api.Controllers.UsersApplications
{
    [Route("[controller]")]
    [ApiController]
    public class UserApplicationController : Controller
    {
        IUserApplicationService _userApplicationService;
        public UserApplicationController(IUserApplicationService userApplicationService) {
            _userApplicationService = userApplicationService;
        }

        [HttpGet("to-edit/{userApplicationId}")]
        public IResult GetToEdit(int userApplicationId)
        {
            var resume = _userApplicationService.GetUserApplicationToEdit(userApplicationId);
            return Results.Ok(resume);
        }

        [HttpGet("resume")]
        public IResult GetResume()
        {
            var resume = _userApplicationService.GetAllResume();
            return Results.Ok(resume);
        }

        [HttpGet("grid")]
        public IResult GetGrid()
        {
            var grid = _userApplicationService.GetGrid();
            return Results.Ok(grid);
        }

        [HttpPost()]
        public IResult Create(CreateUserApplicationDto createDto)
        {
            var id = _userApplicationService.CreateUserApplication(createDto);
            return Results.Ok(id);
        }

        [HttpPut()]
        public IResult Update(UpdateUserApplicationDto update)
        {
            _userApplicationService.Update(update);
            return Results.Ok("Updated");
        }

        [HttpDelete("{userApplicationId}")]
        public IResult Delete(int userApplicationId)
        {
            _userApplicationService.Delete(userApplicationId);
            return Results.Ok("Eliminado");
        }
    }
}
