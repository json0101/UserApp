using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.Actions;
using UserApp.Service.Services.Actions.Dtos;

namespace UserApp.Api.Controllers.Actions
{
    [Route("[controller]")]
    [ApiController]
    public class ActionController : Controller
    {
        IActionService _actionService;
        public ActionController(IActionService actionService) {
            _actionService = actionService;
        }

        [HttpGet("to-edit/{actionId}")]
        public IResult GetToEdit(int actionId)
        {
            var resume = _actionService.GetActionToEdit(actionId);
            return Results.Ok(resume);
        }

        [HttpGet("resume")]
        public IResult GetResume()
        {
            var resume = _actionService.GetAllResume();
            return Results.Ok(resume);
        }

        [HttpGet("grid")]
        public IResult GetGrid()
        {
            var grid = _actionService.GetGrid();
            return Results.Ok(grid);
        }

        [HttpPost()]
        public IResult Create(CreateActionDto createDto)
        {
            var id = _actionService.CreateAction(createDto);
            return Results.Ok(id);
        }

        [HttpPut()]
        public IResult Update(UpdateActionDto update)
        {
            _actionService.Update(update);
            return Results.Ok("Updated");
        }

        [HttpDelete("{actionId}")]
        public IResult Delete(int actionId)
        {
            _actionService.Delete(actionId);
            return Results.Ok("Eliminado");
        }
    }
}
