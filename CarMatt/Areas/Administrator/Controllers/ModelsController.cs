using CarMatt.Data.DTOs.CarModelModule;
using CarMatt.Data.Models;
using CarMatt.Data.Services.CarMakeModule;
using CarMatt.Data.Services.CarModelModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ModelsController : Controller
    {
        private readonly IModelService  modelService;

        private readonly ICarMakeService  carMakeService;

        private readonly UserManager<AppUser> userManager;
        public ModelsController(IModelService modelService, UserManager<AppUser> userManager, ICarMakeService carMakeService)
        {
            this.modelService = modelService;

            this.userManager = userManager;

            this.carMakeService = carMakeService;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.Makes = await carMakeService.GetAll();

            return View();
        }
        public async Task<ActionResult> GetModels()
        {
            var expenses = await modelService.GetAll();

            return Json(new { data = expenses });
        }
        public async Task<ActionResult> Create(ModelDTO modelDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                modelDTO.CreatedBy = user.Id;

                var results = await modelService.Create(modelDTO);

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
                var data = await modelService.GetById(Id);

                if (data != null)
                {
                    ModelDTO file = new ModelDTO()
                    {
                        Id = data.Id,

                        Name = data.Name,

                        CarMakeId = data.CarMakeId,

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
                var results = await modelService.Delete(Id);

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
        public async Task<ActionResult> Update(ModelDTO modelDTO)
        {
            try
            {
                var results = await modelService.Update(modelDTO);

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
