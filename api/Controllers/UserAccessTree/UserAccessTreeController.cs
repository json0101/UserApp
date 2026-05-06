using Microsoft.AspNetCore.Mvc;
using UserApp.Service.Services.UserAccessTree;

namespace UserApp.Api.Controllers.UserAccessTree
{
    [Route("[controller]")]
    [ApiController]
    public class UserAccessTreeController : ControllerBase
    {
        private readonly IUserAccessTreeService _userAccessTreeService;

        public UserAccessTreeController(IUserAccessTreeService userAccessTreeService)
        {
            _userAccessTreeService = userAccessTreeService;
        }

        [HttpGet("{userId}")]
        public IResult GetByUser(int userId, [FromQuery] int? roleId, [FromQuery] int? applicationId)
        {
            var accessTree = _userAccessTreeService.GetByUser(userId, roleId, applicationId);

            return Results.Ok(accessTree);
        }
    }
}
