using Microsoft.AspNetCore.Mvc;

namespace lab.dotnet7.webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class MetricsSampleController : Controller
{
    [HttpPost("counter")]
    public IActionResult Counter()
    {
        return this.Ok();
    }

    [HttpPost("histogram")]
    public IActionResult Histogram()
    {
        return this.Ok();
    }

    [HttpPost("observableGauge")]
    public IActionResult ObservableGauge()
    {
        return this.Ok();
    }
}