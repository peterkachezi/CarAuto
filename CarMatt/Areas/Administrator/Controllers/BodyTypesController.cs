using CarMatt.Data.DTOs.BodyTypeModule;
using CarMatt.Data.Models;
using CarMatt.Data.Services.BodyTypeModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class BodyTypesController : Controller
    {
        private readonly IBodyTypeService  bodyTypeService;

        private readonly UserManager<AppUser> userManager;
        public BodyTypesController(UserManager<AppUser> userManager, IBodyTypeService bodyTypeService)
        {
            this.userManager = userManager;

            this.bodyTypeService = bodyTypeService;
        }
        public ActionResult Index()
        {

            return View();
        }
        public async Task<ActionResult> GetMakes()
        {
            var expenses = await bodyTypeService.GetAll();

            return Json(new { data = expenses });
        }
        public async Task<ActionResult> Create(BodyTypeDTO bodyTypeDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                bodyTypeDTO.CreatedBy = user.Id;

                var results = await bodyTypeService.Create(bodyTypeDTO);

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
                var data = await bodyTypeService.GetById(Id);

                if (data != null)
                {
                    BodyTypeDTO file = new BodyTypeDTO()
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
                var results = await bodyTypeService.Delete(Id);

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
        public async Task<ActionResult> Update(BodyTypeDTO bodyTypeDTO)
        {
            try
            {
                var results = await bodyTypeService.Update(bodyTypeDTO);

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
