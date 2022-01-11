using CarMatt.Data.DTOs.AnalyticsModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.AnalyticsModule
{
    public class AnaltyicsService : IAnaltyicsService
    {
        private readonly ApplicationDbContext context;

        public AnaltyicsService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<AnalyticDTO>> GetAll()
        {
            try
            {
                var analytic = await context.Analytics.ToListAsync();

                var analytics = new List<AnalyticDTO>();

                foreach (var item in analytic)
                {
                    var data = new AnalyticDTO
                    {
                        Id = item.Id,

                        MacID = item.MacID,

                        IPAddress = item.IPAddress,

                        AccessDate = item.AccessDate,
                    };

                    analytics.Add(data);
                }

                return analytics;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
