using Microsoft.AspNetCore.Mvc;

namespace LocalStack.Api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class HealthcheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Description = "Application is Running" });
        }
    }
}
