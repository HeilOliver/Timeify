using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timeify.Common.DI;

namespace Timeify.Api.Controllers
{
    [Authorize(Roles = "api_access, user")]
    [Route("api/[controller]")]
    [ApiController]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class ProtectedController : ControllerBase
    {
        // GET api/protected/home
        [HttpGet]
        public IActionResult Home()
        {
            return new OkObjectResult(new {result = true});
        }
    }
}