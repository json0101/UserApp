using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.Users;
using UserApp.Service.Services.Users.Dto;

namespace UserApp.Api.Controllers.Users
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        IUserService _userService;
        public UserController(IUserService userService) { 
            _userService = userService;
        }

        [HttpGet]
        public IResult GetGrid()
        {
            var users = _userService.GetGrid();

            return Results.Ok(users);
        }

        [HttpGet("toedit/{userId}")]
        public IResult GetToEdit(int userId)
        {
            var user = _userService.GetToEdit(userId);

            return Results.Ok(user);
        }

        [HttpPost]
        public IResult Create(CreateUserDto createUserDto)
        {
            var id = _userService.Create(createUserDto);

            return Results.Ok(id);
        }

        [HttpPut]
        public IResult Edit(UpdateUserDto updateUserDto) { 
            _userService.Update(updateUserDto);

            return Results.Ok("OK");
        }

        [HttpPut("changePassword")]
        public IResult ChangePassword(ChangePasswordDto dto)
        {
            _userService.ChangePassword(dto);

            return Results.Ok("OK");
        }

        [HttpDelete("{userId}")]
        public IResult Delete(int userId)
        {
            _userService.Delete(userId);

            return Results.Ok("Deleted");
        }
    }
}
