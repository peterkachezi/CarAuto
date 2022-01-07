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
using CarMatt.Data.Services.CarMakeModule;
using CarMatt.Data.Services.CarModelModule;
using CarMatt.Data.Services.BodyTypeModule;
using CarMatt.Data.DTOs.VehicleModule;

using CarMatt.EmailServiceModule;
using CarMatt.Data.DTOs.SubscriptionModule;
using CarMatt.Data.Services.SubscriptionModule;
using CarMatt.Data.DTOs.InquiryModule;
using CarMatt.Data.Services.InquiryModule;
using CarMatt.Data.Services.PartModule;

namespace CarMatt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleService vehicleService;

        private readonly ICarImageService carImageService;

        private readonly IFeedBackService feedBackService;

        private readonly IMessagingService messagingService;

        private readonly ICarMakeService carMakeService;

        private readonly IModelService modelService;

        private readonly IBodyTypeService bodyTypeService;

        private readonly IEmailService emailService;

        private readonly ISubscriptionService subscriptionService;

        private readonly IInquiryService inquiryService;

        private readonly IPartService partService;


        public HomeController(IPartService partService, IInquiryService inquiryService, ISubscriptionService subscriptionService, IEmailService emailService, IBodyTypeService bodyTypeService, IModelService modelService, ICarMakeService carMakeService, IMessagingService messagingService, IVehicleService vehicleService, ICarImageService carImageService, IFeedBackService feedBackService)
        {
            this.vehicleService = vehicleService;

            this.carImageService = carImageService;

            this.feedBackService = feedBackService;

            this.messagingService = messagingService;

            this.modelService = modelService;

            this.carMakeService = carMakeService;

            this.bodyTypeService = bodyTypeService;

            this.emailService = emailService;

            this.subscriptionService = subscriptionService;

            this.inquiryService = inquiryService;

            this.partService = partService;

        }

        public async Task<IActionResult> Index(Guid makeId, Guid bodyTypeId, string yearOfProduction, decimal pricefrom, decimal priceto)
        {
            try
            {

                if (makeId == null || makeId == Guid.Empty || bodyTypeId == null || bodyTypeId == Guid.Empty ||
                    yearOfProduction == null || pricefrom.Equals(null) || priceto.Equals(null))
                {
                    var all = (await carImageService.GetAll()).ToList();

                    var result = all.GroupBy(test => test.VehicleId).Select(grp => grp.First()).ToList();

                    ViewBag.Body = await bodyTypeService.GetAll();

                    ViewBag.Make = await carMakeService.GetAll();

                    return View(result);
                }
                else
                {
                    var all = (await carImageService.GetAll()).Where(x => x.MakeId == makeId && x.BodyTypeId == bodyTypeId && x.YearOfProduction == yearOfProduction);

                    var filterPrice = all.Where(p => p.Price >= pricefrom && p.Price <= priceto).ToList();

                    var result = filterPrice.GroupBy(test => test.VehicleId).Select(grp => grp.First()).ToList();

                    ViewBag.Body = await bodyTypeService.GetAll();

                    ViewBag.Make = await carMakeService.GetAll();

                    return View(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

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
        public async Task<ActionResult> Inquiry(InquiryDTO inquiryDTO)
        {
            try
            {
                var results = await inquiryService.Create(inquiryDTO);

                if (results != null)
                {
                    var sendemail = messagingService.InquirySMSAlert(inquiryDTO);

                    return Json(new { success = true, responseText = "Your message has been  successfully sent" });
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
        public async Task<ActionResult> Subscription(SubscriptionDTO subscriptionDTO)
        {
            try
            {
                var results = await subscriptionService.Create(subscriptionDTO);

                if (results != null)
                {
                    var sendemail = emailService.SendSubscriptionNotification(subscriptionDTO);

                    return Json(new { success = true, responseText = "Your have successfully subscribed" });
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

        public ActionResult ContactUs()

        {
            return View();
        }


        public async Task<IActionResult> SearchCars(Guid makeId, Guid bodyTypeId, string yearOfProduction, decimal pricefrom, decimal priceto)
        {
            try
            {
                if (makeId == null || makeId == Guid.Empty || bodyTypeId == null || bodyTypeId == Guid.Empty || yearOfProduction == null || pricefrom.Equals(null) || priceto.Equals(null))

                {
                    var all = (await carImageService.GetAll()).ToList();

                    var result = all.GroupBy(test => test.VehicleId).Select(grp => grp.First()).ToList();

                    ViewBag.Make = await carMakeService.GetAll();

                    return View(result);
                }
                else
                {
                    var all = (await carImageService.GetAll()).Where(x => x.MakeId == makeId && x.BodyTypeId == bodyTypeId && x.YearOfProduction == yearOfProduction);

                    var filterPrice = all.Where(p => p.Price >= pricefrom && p.Price <= priceto).ToList();

                    var result = filterPrice.GroupBy(test => test.VehicleId).Select(grp => grp.First()).ToList();

                    ViewBag.Make = await carMakeService.GetAll();

                    return View(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public async Task<ActionResult> Parts()
        {
            var all = (await partService.GetPartsAndImages()).ToList();

            var parts = all.GroupBy(test => test.PartId).Select(grp => grp.First()).ToList();

            return View(parts);
        }



        public async Task<ActionResult> VehicleDetails(Guid Id)
        {
            CombinedDTO combinedDTO = new CombinedDTO()
            {
            };

            combinedDTO.vehicleDTO = await vehicleService.GetById(Id);

            combinedDTO.singleImageDTO = await carImageService.GetByCardIdSingle(Id);

            combinedDTO.imageDTO = await carImageService.GetByCarId(Id);

            var all = (await carImageService.GetAll()).Where(x => x.ModelName == combinedDTO.vehicleDTO.ModelName);

            combinedDTO.similarvehicleDTO = all.GroupBy(test => test.VehicleId).Select(grp => grp.First()).ToList();

            return View(combinedDTO);
        }
    }
}
