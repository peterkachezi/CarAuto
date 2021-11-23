using CarMatt.Data.DTOs.VehicleModule;
using CarMatt.Data.Models;
using CarMatt.Data.Services.VehicleModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CarsController : Controller
    {
  
        private readonly IVehicleService  vehicleService;

        private readonly UserManager<AppUser> userManager;
        public CarsController(IVehicleService vehicleService, UserManager<AppUser> userManager)
        {
            this.vehicleService = vehicleService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CarRegistration()
        {
            return View();
        }

        public async Task<IActionResult> GetExpenseType()
        {
            var expenses = await vehicleService.GetAll();

            return Json(new { data = expenses });
        }
        public async Task<IActionResult> Create(VehicleDTO  vehicleDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                vehicleDTO.CreatedBy = user.Id;

                var results = await vehicleService.Create(vehicleDTO);

                if (results !=null)
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

                        Make = data.Make,

                        AvailabilityStatus = data.AvailabilityStatus,

                        Model = data.Model,

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

                if (results ==true)
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
        public async Task<IActionResult> Update(VehicleDTO vehicleDTO)
        {
            try
            {
                var results = await vehicleService.Update(vehicleDTO);

                if (results !=null)

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



    }
}
