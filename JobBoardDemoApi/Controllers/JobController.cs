using Microsoft.AspNetCore.Mvc;

namespace JobBoardDemoApi.Controllers;

public class JobController : Controller
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}