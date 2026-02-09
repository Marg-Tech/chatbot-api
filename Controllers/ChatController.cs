using Microsoft.AspNetCore.Mvc;

namespace ChatPlatform.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("OK");
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
       var version = typeof(Program).Assembly.GetName().Version?.ToString();
     return Ok(version);
    }
}
