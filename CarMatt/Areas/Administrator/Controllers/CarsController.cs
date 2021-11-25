using CarMatt.Data.DTOs.ImageModule;
using CarMatt.Data.DTOs.VehicleModule;
using CarMatt.Data.Models;
using CarMatt.Data.Services.CarMakeModule;
using CarMatt.Data.Services.CarModelModule;
using CarMatt.Data.Services.ImageModule;
using CarMatt.Data.Services.VehicleModule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

            var productsList = (from product in make
                                select new SelectListItem()
                                {
                                    Text = product.Name,
                                    Value = product.Id.ToString(),
                                }).ToList();

            productsList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

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
        public async Task<IActionResult> UploadFilesAjax(VehicleDTO vehicleDTO)

        {
            var user = await userManager.FindByEmailAsync(User.Identity.Name);

            ImageDTO file = new ImageDTO();

            file.CreatedBy = user.Id;

            vehicleDTO.CreatedBy = user.Id;

            //< init >

            long uploaded_size = 0;

            string path_for_Uploaded_Files = env.WebRootPath + "\\uploads\\";

            //</ init >



            //< get form_files >

            //IFormFile uploaded_File

            var uploaded_files = Request.Form.Files;

            var myfiles = new List<ImageDTO>();

            foreach (var item in uploaded_files)
            {
                var data = new ImageDTO
                {
                    ImageName = item.FileName,
                    CreatedBy = user.Id,
                };

                myfiles.Add(data);
            }

            var result = vehicleService.SaveUploads(myfiles, vehicleDTO);

            //</ get form_files >



            //------< @Loop: Uploaded Files >------

            int iCounter = 0;

            string sFiles_uploaded = "";

            foreach (var uploaded_file in uploaded_files)

            {
                //----< Uploaded File >----

                iCounter++;

                uploaded_size += uploaded_file.Length;

                sFiles_uploaded += "\n" + uploaded_file.FileName;


                //< Filename >

                string uploaded_Filename = uploaded_file.FileName;

                string new_Filename_on_Server = path_for_Uploaded_Files + "\\" + uploaded_Filename;

                //</ Filename >


                //< Copy File to Target >

                using (FileStream stream = new FileStream(new_Filename_on_Server, FileMode.Create))

                {

                    await uploaded_file.CopyToAsync(stream);

                }

                //< Copy File to Target >

                //----</ Uploaded File >----

            }

            //------</ @Loop: Uploaded Files >------

            //string message = "Upload successful!\n files uploaded:" + iCounter + "\nsize:" + uploaded_size + "\n" + sFiles_uploaded;

          

            if (result != null)
            {
                return Json(new { success = true, responseText = "The vehicle has been successfully registered" });
            }

            else
            {
                return Json(new { success = false, responseText = "Unable to registered the vehicle" });
            }


        }

    }
}
