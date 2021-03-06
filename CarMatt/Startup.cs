

using CarMatt.Data.Models;
using CarMatt.Data.Services.AgentModule;
using CarMatt.Data.Services.AnalyticsModule;
using CarMatt.Data.Services.BodyTypeModule;
using CarMatt.Data.Services.CarMakeModule;
using CarMatt.Data.Services.CarModelModule;
using CarMatt.Data.Services.CountyModule;
using CarMatt.Data.Services.FeedBackModule;
using CarMatt.Data.Services.ImageModule;
using CarMatt.Data.Services.InquiryModule;
using CarMatt.Data.Services.PartModule;
using CarMatt.Data.Services.SMSModule;
using CarMatt.Data.Services.SubscriptionModule;
using CarMatt.Data.Services.VehicleModule;
using CarMatt.EmailServiceModule;
using CarMatt.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Management;
using System.Net;

namespace CarMatt
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            services.AddRazorPages();

            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Transient);

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ApplicationUserClaimsPrincipalFactory>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICountyService, CountyService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<ICarMakeService, CarMakeService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<ICarImageService, CarImageService>();
            services.AddScoped<IBodyTypeService, BodyTypeService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<IMessagingService, MessagingService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IInquiryService, InquiryService>();
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IAnaltyicsService, AnaltyicsService>();

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(ApplicationDbContext context ,IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            CreateRoles(roleManager);

            Analytics(context);

            CreateRolesandUsersAsync(userManager);

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "Administrator",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "Agent",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            });

        }

        private async void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!roleManager.RoleExistsAsync("Administrator").Result)

                {
                    var role = new IdentityRole();

                    role.Name = "Administrator";

                    await roleManager.CreateAsync(role);
                }

                if (!roleManager.RoleExistsAsync("Agent").Result)
                {
                    var role = new IdentityRole();

                    role.Name = "Agent";

                    await roleManager.CreateAsync(role);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }

        private async void CreateRolesandUsersAsync(UserManager<AppUser> userManager)
        {
            try
            {
                var admin = await userManager.FindByEmailAsync("admin@gmail.com");

                if (admin == null)
                {
                    var user = new AppUser();

                    user.UserName = "admin@gmail.com";
                    user.Email = "admin@gmail.com";
                    user.PhoneNumber = "0704509484";
                    user.FullName = "Peter Admin";
                    user.EmailConfirmed = true;
                    user.isActive = true;
                    user.CreateDate = DateTime.Now;
                    string userPWD = "Admin@2021";

                    var chkUser = await userManager.CreateAsync(user, userPWD);

                    if (chkUser.Succeeded == true)
                    {
                        userManager.AddToRoleAsync(user, "Administrator").Wait();

                    }

                }

                var agent = await userManager.FindByEmailAsync("agent@gmail.com");

                if (agent == null)
                {
                    var user = new AppUser();

                    user.UserName = "agent@gmail.com";
                    user.Email = "agent@gmail.com";
                    user.PhoneNumber = "0704509484";
                    user.FullName = "Peter Agent";
                    user.EmailConfirmed = true;
                    user.isActive = true;
                    user.CreateDate = DateTime.Now;
                    string userPWD = "Agent@2021";

                    var chkUser = await userManager.CreateAsync(user, userPWD);

                    if (chkUser.Succeeded == true)
                    {
                        userManager.AddToRoleAsync(user, "Agent").Wait();

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Analytics(ApplicationDbContext context)
        { 
            //For IpAddrress
            IPHostEntry iphostinfo = Dns.GetHostEntry(Dns.GetHostName());

            string IpAddress = Convert.ToString(iphostinfo.AddressList.FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));
            //For MacId

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

            ManagementObjectCollection moc = mc.GetInstances();

            string MACAdress = string.Empty;

            foreach (ManagementObject mo in moc)
            {
                if (MACAdress == string.Empty) //only return MAC Address from first card
                {
                    if ((bool)mo["IPEnabled"] == true) MACAdress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }

            MACAdress = MACAdress.Replace(":", "-");

            //Add to database

            Analytic client = new Analytic();

            client.Id = Guid.NewGuid();

            client.MacID = MACAdress;

            client.IPAddress = IpAddress;

            client.AccessDate = DateTime.Now;

            context.Analytics.Add(client);

            context.SaveChanges();

        }
    }
}
