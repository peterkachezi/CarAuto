using CarMatt.Data.DTOs.AnalyticsModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.AnalyticsModule
{
    public interface IAnaltyicsService
    {
        Task<List<AnalyticDTO>> GetAll();
    }
}