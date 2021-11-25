using CarMatt.Data.DTOs.CarMakeModule;
using CarMatt.Data.Models;
using CarMatt.Data.Services.CarMakeModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class MakesController : Controller
    {

        private readonly ICarMakeService carMakeService;

        private readonly UserManager<AppUser> userManager;
        public MakesController(UserManager<AppUser> userManager, ICarMakeService carMakeService)
        {
            this.userManager = userManager;

            this.carMakeService = carMakeService;
        }
        public ActionResult Index()
        {        

            return View();
        }
        public async Task<ActionResult> GetMakes()
        {
            var expenses = await carMakeService.GetAll();

            return Json(new { data = expenses });
        }
        public async Task<ActionResult> Create(CarMakeDTO carMakeDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                carMakeDTO.CreatedBy = user.Id;

                var results = await carMakeService.Create(carMakeDTO);

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
        public async Task<ActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await carMakeService.GetById(Id);

                if (data != null)
                {
                    CarMakeDTO file = new CarMakeDTO()
                    {
                        Id = data.Id,

                        Name = data.Name,

                        CreatedBy = data.CreatedBy,

                        CreatedByName = data.CreatedByName,

                        CreateDate = data.CreateDate,
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
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await carMakeService.Delete(Id);

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
        public async Task<ActionResult> Update(CarMakeDTO carMakeDTO)
        {
            try
            {
                var results = await carMakeService.Update(carMakeDTO);

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
    }
}
