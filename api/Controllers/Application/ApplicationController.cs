using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.Application;

namespace UserApp.Api.Controllers.Application
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : Controller
    {
        IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService) {
            _applicationService = applicationService;
        }

        [HttpGet()]
        public IResult GetAll()
        {
                return Results.Ok(_applicationService.GetAll());
        }
    }
}
