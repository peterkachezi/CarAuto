using Microsoft.AspNetCore.Mvc;

namespace CarMatt.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
