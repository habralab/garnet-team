using Microsoft.AspNetCore.Mvc;

namespace Garnet.Controllers;

[ApiController]
[Route("api/v1")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    [Route("up")]
    public string Up()
    {
        return "Healthy";
    }
}