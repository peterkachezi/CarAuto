using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Threading.Tasks;
using CarMatt.Data.Models;
using CarMatt.Data.Services.AnalyticsModule;
using Microsoft.AspNetCore.Mvc;

namespace CarMatt.Controllers
{
    
    public class AnalyticsController : Controller
    {
        private readonly IAnaltyicsService  analtyicsService;

        public AnalyticsController(IAnaltyicsService analtyicsService)
        {
            this.analtyicsService = analtyicsService;
        }
        public IActionResult Index()
        {     

            return View();
        }
    }
}
