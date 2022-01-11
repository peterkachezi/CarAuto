using CarMatt.Data.Services.AnalyticsModule;
using CarMatt.Data.Services.PartModule;
using CarMatt.Data.Services.VehicleModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class DashboardController : Controller
    {
        private readonly IAnaltyicsService analtyicsService;

        private readonly IVehicleService  vehicleService;

        private readonly IPartService  partService;

        public DashboardController(IPartService partService,IAnaltyicsService analtyicsService, IVehicleService vehicleService)
        {
            this.analtyicsService = analtyicsService;
            this.vehicleService = vehicleService;
            this.partService = partService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Parts = (await partService.GetAllParts()).Count();

            ViewBag.Cars = (await vehicleService.GetAll()).Count();

            ViewBag.Visitors = (await analtyicsService.GetAll()).Count();

            return View();
        }
    }
}
