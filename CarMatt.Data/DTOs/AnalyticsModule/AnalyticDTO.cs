using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.AnalyticsModule
{
    public class AnalyticDTO
    {
        public Guid Id { get; set; }
        public string MacID { get; set; }
        public string IPAddress { get; set; }
        public DateTime AccessDate { get; set; }
    }
}
