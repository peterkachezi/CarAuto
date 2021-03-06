using CarMatt.Data.DTOs.AgentModule;
using CarMatt.Data.DTOs.ApplicationUserServiceModule;
using CarMatt.Data.Models;
using CarMatt.Data.Services.AgentModule;
using CarMatt.Data.Services.CountyModule;
using CarMatt.Data.Services.SMSModule;
using CarMatt.EmailServiceModule;
using CarMatt.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PasswordOptions = CarMatt.Helpers.PasswordOptions;

namespace CarMatt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        private readonly SignInManager<AppUser> signInManager;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IEmailService emailService;

        private readonly IMessagingService  messagingService;

        private readonly IAgentService  agentService;

        private readonly ICountyService  countyService;


        private readonly IConfiguration config;

        private IWebHostEnvironment env;


        public UserManagerController(IMessagingService messagingService,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,

        RoleManager<IdentityRole> roleManager, IEmailService emailService,  IConfiguration config, IWebHostEnvironment env, IAgentService agentService, ICountyService countyService)
        {
            this.userManager = userManager;

            this.signInManager = signInManager;

            this.roleManager = roleManager;

            this.agentService = agentService;

            this.emailService = emailService;

            this.countyService = countyService;

            this.config = config;

            this.env = env;

            this.messagingService = messagingService;

        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Roles = await roleManager.Roles.ToListAsync();

            ViewBag.Counties = await countyService.GetAll();
              
            return View();
        }  
        
        public IActionResult UserProfile()
        {
         

            return View();
        }


        public async Task<ActionResult> GetUsers()
        {
            var doctor = (await userManager.Users.ToListAsync());

            var doctors = new List<ApplicationUserDTO>();

            foreach (var item in doctor)
            {
                var data = new ApplicationUserDTO
                {
                    Id = item.Id,

                    Email = item.Email,

                    FullName = item.FullName,

                    PhoneNumber = item.PhoneNumber,

                    CreateDate = item.CreateDate,

                    isActive = item.isActive,

                };

                doctors.Add(data);
            }

            return Json(new { data = doctor});

        }
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterDTO registerDTO)
        {
            try
            {
                string password = PasswordStore.GenerateRandomPassword(new PasswordOptions
                {
                    RequiredLength = 8,

                    RequireNonLetterOrDigit = true,

                    RequireDigit = true,

                    RequireLowercase = true,

                    RequireUppercase = true,

                    RequireNonAlphanumeric = true,

                    RequiredUniqueChars = 1
                });

                registerDTO.Password = password;


                var user = new AppUser()
                {
                    UserName = registerDTO.Email,

                    Email = registerDTO.Email,

                    isActive = true,

                    PhoneNumber = registerDTO.PhoneNumber,

                    FullName = registerDTO.FullName,

                    CreateDate = DateTime.Now,

                };

                var result = await userManager.CreateAsync(user, registerDTO.Password);

                var getloggedUser = await userManager.FindByEmailAsync(User.Identity.Name);

                registerDTO.CreatedBy = getloggedUser.Id;


                var data = new AgentDTO
                {
                    Id = Guid.Parse(user.Id),

                    FirstName = registerDTO.FirstName,

                    LastName = registerDTO.LastName,

                    Email = registerDTO.Email,

                    PhoneNumber = registerDTO.PhoneNumber,

                    CreatedBy = registerDTO.CreatedBy,

                    Gender = registerDTO.Gender,

                    Town = registerDTO.Town,

                    County = registerDTO.County,

                };

                var createEmployer = await agentService.Create(data);

                var sendEmail = emailService.SendAccountCreationEmailNotification(registerDTO);

                var sms = messagingService.usersAccount(registerDTO);

                var createRole = await userManager.AddToRoleAsync(user, registerDTO.RoleName);

                if (result.Succeeded)
                {
                    return Json(new { success = true, responseText = "Account has been created successfully" });
                }

                foreach (var error in result.Errors)
                {
                    return Json(new { success = false, responseText = "Unable to update record report details" });

                }

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        //public async Task<IActionResult> ActivateAccount(string Id, RegisterDTO registerDTO)
        //{
        //    try
        //    {
        //        var user = await userManager.FindByIdAsync(Id);

        //        user.isActive = true;

        //        var activate = await userManager.UpdateAsync(user);

        //        registerDTO.Email = user.Email;

        //        registerDTO.FullNames = user.FullName;

        //        registerDTO.Message = "Your account has been activated";

        //        var sendMail = emailService.SendActivationDeactivationNotification(registerDTO);

        //        return Json(new { success = true, responseText = "Account has been activated successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }

        //}


        //public async Task<IActionResult> DeactivateAccount(string Id, RegisterDTO registerDTO)
        //{
        //    try
        //    {
        //        var user = await userManager.FindByIdAsync(Id);

        //        var getUserRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();

        //        if (getUserRole == "Administrator")
        //        {
        //            return Json(new { success = false, responseText = "Unable to deactivate Administrator Account" });

        //        }
        //        user.isActive = false;

        //        registerDTO.Email = user.Email;

        //        registerDTO.FullNames = user.FullName;

        //        registerDTO.Message = "Your account has been deactivated";

        //        var sendMail = emailService.SendActivationDeactivationNotification(registerDTO);

        //        var activate = await userManager.UpdateAsync(user);

        //        return Json(new { success = true, responseText = "Account has been deactivate successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }

        //}

        public async Task<ActionResult> GetUserById(string Id)
        {
            try
            {
                var data = await userManager.FindByIdAsync(Id);

                if (data != null)
                {
                    RegisterDTO file = new RegisterDTO()
                    {
                        Id = data.Id,

                        Email = data.Email,

                        PhoneNumber = data.PhoneNumber,

                        FullNames = data.FullName,

                    };

                    return Json(new { Data = file });
                }

                return Json(new { Data = false });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> UpdateUserDetails(string Id, RegisterDTO registerDTO)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id);

                var getUserRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();

                user.FullName = registerDTO.FullNames;

                var update = await userManager.UpdateAsync(user);

                return Json(new { success = true, responseText = "Account has been updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
    }
}
