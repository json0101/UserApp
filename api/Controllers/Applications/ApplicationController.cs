using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.Applications;
using UserApp.Service.Services.Applications.Dtos;

namespace UserApp.Api.Controllers.Applications
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : Controller
    {
        IApplicationService _appService;
        public ApplicationController(IApplicationService appService) {
            _appService = appService;
        }

        [HttpGet("to-edit/{appId}")]
        public IResult GetToEdit(int appId)
        {
            var resume = _appService.GetApplicationToEdit(appId);
            return Results.Ok(resume);
        }

        [HttpGet("resume")]
        public IResult GetResume()
        {
            var resume = _appService.GetAllResume();
            return Results.Ok(resume);
        }

        [HttpGet("grid")]
        public IResult GetGrid()
        {
            var grid = _appService.GetGrid();
            return Results.Ok(grid);
        }

        [HttpPost()]
        public IResult Create(CreateApplicationDto createDto)
        {
            var id = _appService.CreateApplication(createDto);
            return Results.Ok(id);
        }

        [HttpPut()]
        public IResult Update(UpdateApplicationDto update)
        {
            _appService.Update(update);
            return Results.Ok("Updated");
        }

        [HttpDelete("{appId}")]
        public IResult Delete(int appId)
        {
            _appService.Delete(appId);
            return Results.Ok("Eliminado");
        }
    }
}
