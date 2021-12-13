using CarMatt.Data.Services.SubscriptionModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionService subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> GetSubscriptions()
        {
            var subscription = await subscriptionService.GetAll();

            return Json(new { data = subscription });
        }




    }
}
