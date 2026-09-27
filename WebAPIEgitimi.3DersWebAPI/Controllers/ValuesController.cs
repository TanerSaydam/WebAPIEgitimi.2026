using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WebAPIEgitimi._3DersWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("FixedWindowLimiter")]
public class ValuesController : ControllerBase
{
    [HttpGet]
    //[EnableRateLimiting("FixedWindowLimiter")]
    public IActionResult Get()
    {
        return Ok("");
    }
}
