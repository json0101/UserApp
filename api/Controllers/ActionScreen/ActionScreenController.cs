using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.ActionsScreens;
using UserApp.Service.Services.ActionsScreens.Dtos;

namespace UserApp.Api.Controllers.ActionScreen
{
    [Route("[controller]")]
    [ApiController]
    public class ActionScreenController : Controller
    {
        IActionScreenService _actionScreenService;
        public ActionScreenController(IActionScreenService actionScreenService) {
            _actionScreenService = actionScreenService;
        }

        [HttpGet("{actionScreenId}")]
        public IResult Get(int actionScreenId)
        {
            var actionScreen = _actionScreenService.Get(actionScreenId);
            return Results.Ok(actionScreen);
        }

        [HttpGet("grid")]
        public IResult GetGrid()
        {
            var actionScreen = _actionScreenService.GetGrid();
            return Results.Ok(actionScreen);
        }

        [HttpPost()]
        public IResult Create(CreateActionScreenDto create)
        {
            var actionScreen = _actionScreenService.Create(create);
            return Results.Ok(actionScreen);
        }

        [HttpPut()]
        public IResult Edit(UpdateActionScreenDto update)
        {
            _actionScreenService.Update(update);
            return Results.Ok("updated");
        }

        [HttpDelete("{actionScreenId}")]
        public IResult Delete(int actionScreenId)
        {
            _actionScreenService.Delete(actionScreenId);
            return Results.Ok("OK");
        }
    }
}
