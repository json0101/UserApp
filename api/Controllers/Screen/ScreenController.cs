using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.Screens.Dtos;
using UserApp.Service.Services.Screens.Service;

namespace UserApp.Api.Controllers.Screen
{
    [ApiController]
    [Route("[controller]")]
    public class ScreenController : Controller
    {
        IScreenService _screenService;

        public ScreenController(IScreenService screenService) { 
            _screenService = screenService;
        }

        [HttpGet("{id}")]
        public IResult GetOne(int id)
        {
            return Results.Ok(_screenService.GetScreen(id));
        }

        [HttpGet()]
        public IResult GetAll()
        {
            return Results.Ok(_screenService.GetScreens());
        }

        [HttpGet("fathers")]
        public IResult GetAllFathers()
        {
            var fathers = _screenService.GetScreensFathers();
            return Results.Ok(fathers);
        }

        [HttpPost]
        public IResult Save(CreateScreenDto create)
        {
            _screenService.Save(create);
            return Results.Ok("Save");
        }

        [HttpPut]
        public IResult Update(UpdateScreenDto update)
        {
            _screenService.Update(update);
            return Results.Ok("Updated");
        }

        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            _screenService.Delete(id);
            return Results.Ok("Deleted");
        }
    }
}
