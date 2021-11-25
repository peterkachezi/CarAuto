using CarMatt.Data.DTOs;
using CarMatt.Data.DTOs.ImageModule;
using CarMatt.Data.DTOs.VehicleModule;
using CarMatt.Data.Models;
using CarMatt.Data.Services.CarMakeModule;
using CarMatt.Data.Services.CarModelModule;
using CarMatt.Data.Services.ImageModule;
using CarMatt.Data.Services.VehicleModule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CarsController : Controller
    {
        private IWebHostEnvironment env;

        private readonly IVehicleService vehicleService;

        private readonly UserManager<AppUser> userManager;

        private readonly ICarMakeService carMakeService;

        private readonly IModelService modelService;

        private readonly ICarImageService carImageService;
        public CarsController(ICarImageService carImageService, IVehicleService vehicleService, UserManager<AppUser> userManager, IWebHostEnvironment env, ICarMakeService carMakeService, IModelService modelService)
        {
            this.vehicleService = vehicleService;
            this.userManager = userManager;
            this.carMakeService = carMakeService;
            this.modelService = modelService;
            this.carImageService = carImageService;
            this.env = env;
        }
        public ActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = (await vehicleService.GetAll()).OrderBy(x=>x.CreateDate).ToList();

            return Json(new { data = vehicles });
        }
        public async Task<IActionResult> CarRegistration()
        {
            ViewBag.Makes = await carMakeService.GetAll();

            return View();
        }
        public async Task<ActionResult> GetModels(Guid Id)
        {
            var streams = (await modelService.GetAll()).Where(x => x.CarMakeId == Id).ToList();

            return Json(streams.Select(x => new
            {
                MakeId = x.Id,

                MakeName = x.Name

            }).ToList());

        }
        public async Task<IActionResult> GetExpenseType()
        {
            var expenses = await vehicleService.GetAll();

            return Json(new { data = expenses });
        }
        public async Task<IActionResult> Create(VehicleDTO vehicleDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                vehicleDTO.CreatedBy = user.Id;

                var results = await vehicleService.Create(vehicleDTO);

                if (results != null)
                {
                    return Json(new { success = true, responseText = "Record has been successfully saved" });
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
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await vehicleService.GetById(Id);

                if (data != null)
                {
                    VehicleDTO file = new VehicleDTO()
                    {
                        Id = data.Id,

                        Price = data.Price,

                        Quantity = data.Quantity,

                        MakeId = data.MakeId,

                        AvailabilityStatus = data.AvailabilityStatus,

                        ModelId = data.ModelId,

                        Kilometres = data.Kilometres,

                        BodyType = data.BodyType,

                        StyleTrim = data.StyleTrim,

                        Engine = data.Engine,

                        Drivetrain = data.Drivetrain,

                        Transmission = data.Transmission,

                        ExteriorColor = data.ExteriorColor,

                        InteriorColor = data.InteriorColor,

                        Passangers = data.Passangers,

                        FuelType = data.FuelType,

                        CityFuelEconomy = data.CityFuelEconomy,

                        HighWayFuelEconomy = data.HighWayFuelEconomy,

                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy,
                    };

                    return Json(new { data = file });
                }

                return Json(new { data = false });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await vehicleService.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> UpdateInformation(Guid Id)
        {
            var make = await carMakeService.GetAll();
                 
            ViewBag.Makes = make;

            var vehicle = await vehicleService.GetById(Id);

            return View(vehicle);
        }






        public async Task<ActionResult> ViewInformation(Guid Id)
        {
            CombinedDTO combinedDTO = new CombinedDTO()
            {
            };

            combinedDTO.vehicleDTO = await vehicleService.GetById(Id);

            combinedDTO.imageDTO = await carImageService.GetByCarId(Id);

            combinedDTO.singleImageDTO = await carImageService.GetByCardIdSingle(Id);

            return View(combinedDTO);
        }


        public async Task<IActionResult> Update(VehicleDTO vehicleDTO)
        {
            try
            {
                var results = await vehicleService.Update(vehicleDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Record  has been  successfully updated!" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

    
        [HttpPost]
        public async Task<IActionResult> UploadFilesAjax3Async(VehicleDTO  vehicleDTO, IFormFile[] ImageName)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                vehicleDTO.CreatedBy = user.Id;

                if (ImageName == null || ImageName.Length == 0)
                {
                    return Content("File(s) not selected");
                }
                else
                {
                    vehicleDTO.ImageName = new List<string>();

                    foreach (IFormFile photo in ImageName)
                    {
                        var path = Path.Combine(this.env.WebRootPath, "uploads", photo.FileName);

                        var stream = new FileStream(path, FileMode.Create);

                        photo.CopyTo(stream);

                        vehicleDTO.ImageName.Add(photo.FileName);
                    }
                }

                var result = vehicleService.SaveUploads(vehicleDTO);


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
    

    [HttpPost]
        public async Task<IActionResult> UploadFilesAjax2(List<IFormFile> files, VehicleDTO vehicleDTO)
        {
            var user = await userManager.FindByEmailAsync(User.Identity.Name);

            ImageDTO imageDTO = new ImageDTO();

            imageDTO.CreatedBy = user.Id;

            vehicleDTO.CreatedBy = user.Id;

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(file.FileName);

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);

                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);

                        var myfiles = new List<ImageDTO>();

                        //foreach (var item in newFileName)
                        //{
                        //    var data = new ImageDTO
                        //    {
                        //        ImageName = item.newFileName,
                        //        CreatedBy = user.Id,
                        //    };

                        //    myfiles.Add(data);
                        //}


                        // Combines two strings into a path.
                        var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")).Root + $@"\{newFileName}";


                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                    }
                }
            }
            return View("Index");
        }
    }
}
