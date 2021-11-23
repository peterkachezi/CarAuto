using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    public class AgentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
