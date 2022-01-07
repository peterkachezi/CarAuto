using CarMatt.Data.Services.InquiryModule;
using CarMatt.Data.Services.SMSModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class InquiriesController : Controller
    {
        private readonly IInquiryService inquiryService;

        private readonly IMessagingService  messagingService;
        public InquiriesController(IInquiryService inquiryService, IMessagingService messagingService)
        {
            this.inquiryService = inquiryService;

            this.messagingService = messagingService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetInquiries()
        {
            var expenses = await inquiryService.GetAll();

            return Json(new { data = expenses });
        }
    }
}
