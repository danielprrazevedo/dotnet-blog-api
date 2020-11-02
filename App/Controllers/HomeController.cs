using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Json(new { test = "test" });
        }
    }
}