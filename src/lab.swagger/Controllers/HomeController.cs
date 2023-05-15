using Microsoft.AspNetCore.Mvc;

namespace lab.swagger.Controllers;

/// <summary>
/// 首頁
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// ctor
    /// </summary>
    public HomeController()
    {
    }

    /// <summary>
    /// index
    /// </summary>
    /// <returns></returns>
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(new RedirectResult("~/swagger"));
    }
}