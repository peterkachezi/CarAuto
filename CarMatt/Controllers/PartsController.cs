using CarMatt.Data.Services.PartModule;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarMatt.Controllers
{
    public class PartsController : Controller
    {
        private readonly IPartService partService;

        public PartsController(IPartService partService)
        {
            this.partService = partService;
        }
        public async Task<IActionResult> Index()
        {
            var parts = await partService.GetAllParts();

            return View();
        }
    }
}
