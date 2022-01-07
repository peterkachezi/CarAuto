using CarMatt.Data.DTOs.PartModule;
using CarMatt.Data.Models;
using CarMatt.Data.Services.CarMakeModule;
using CarMatt.Data.Services.PartModule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PartsController : Controller
    {
        private readonly IPartService partService;

        private readonly ICarMakeService  carMakeService;

        private readonly UserManager<AppUser> userManager;

        private IWebHostEnvironment env;

        public PartsController(ICarMakeService carMakeService,IPartService partService, UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            this.partService = partService;
            this.userManager = userManager;
            this.carMakeService = carMakeService;
            this.env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RegisterParts()
        {
            ViewBag.Make = await carMakeService.GetAll();

            return View();
        }

        public async Task<IActionResult> GetParts()
        {
            var parts = await partService.GetAllParts();

            return Json(new { data = parts });

        }

        public async Task<IActionResult> Create(PartDTO  partDTO, IFormFile[] ImageName)
        {
            try
            {
                //var isCarExist = await partService.GetById(partDTO.ModelId);

                //if (isCarExist != null)
                //{
                //    return Json(new { success = false, responseText = "This car already exists" });

                //}

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                partDTO.CreatedBy = user.Id;

                if (ImageName == null || ImageName.Length == 0)
                {

                    return Json(new { success = false, responseText = "Please upload photos / images" });

                }
                else
                {
                    partDTO.ImageName = new List<string>();

                    foreach (IFormFile photo in ImageName)
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(photo.FileName);

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);

                        // concatenating  FileName + FileExtension
                        var newFileName = string.Concat(myUniqueFileName, fileExtension);

                        var path = Path.Combine(env.WebRootPath, "partsuploads", newFileName);

                        var stream = new FileStream(path, FileMode.Create);

                        photo.CopyTo(stream);

                        partDTO.ImageName.Add(newFileName);
                    }
                }

                var result = partService.Create(partDTO);

                if (result != null)
                {
                    return Json(new { success = true, responseText = "The vehicle has been successfully registered" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Unable to registered the vehicle" });
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
