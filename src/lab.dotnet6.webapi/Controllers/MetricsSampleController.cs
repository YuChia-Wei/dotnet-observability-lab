using Microsoft.AspNetCore.Mvc;
using observability.component.Tracing;

namespace lab.dotnet6.webapi.Controllers;

[ApiController]
[Route("[controller]")]
[MethodTracing]
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