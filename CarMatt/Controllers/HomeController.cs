using CarMatt.Data.Services.ImageModule;
using CarMatt.Data.Services.VehicleModule;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using CarMatt.Data.DTOs.FeedBackModule;
using Microsoft.AspNetCore.Identity;
using CarMatt.Data.Services.FeedBackModule;
using System;
using CarMatt.Data.Services.SMSModule;
using Microsoft.EntityFrameworkCore.Internal;

namespace CarMatt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleService vehicleService;
        private readonly ICarImageService carImageService;
        private readonly IFeedBackService feedBackService;
        private readonly IMessagingService messagingService;

        public HomeController(IMessagingService messagingService, IVehicleService vehicleService, ICarImageService carImageService, IFeedBackService feedBackService)
        {
            this.vehicleService = vehicleService;

            this.carImageService = carImageService;

            this.feedBackService = feedBackService;

            this.messagingService = messagingService;
        }
        public async Task<IActionResult> Index()
        {
           // var vehicles = (await vehicleService.GetAll()).ToList();

            var vehicles = (await vehicleService.GetAll()).Select(p => p.Id).ToList();

            var all = (await carImageService.GetAll()).Where(e => vehicles.Contains(e.VehicleId)).ToList();

            var images = all.GroupBy(x => x.VehicleId).Select(g => g.First());
   



            return View(vehicles);
        }

        public async Task<ActionResult> CustomerFeedBack(FeedBackDTO feedBackDTO)
        {
            try
            {
                var results = await feedBackService.Create(feedBackDTO);

                if (results != null)
                {
                    var sendsms = await messagingService.FeedBackSMSAlert(feedBackDTO);

                    return Json(new { success = true, responseText = "Your message has been successfully received ,one of our agents will attend to you shortly" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Record has been  not been saved" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

    }
}
