using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.ImageModule
{
    public class ImageDTO
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public string ImageName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
