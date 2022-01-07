using CarMatt.Data.DTOs.ImageModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.VehicleModule
{
    public class CombinedDTO
    {
        public List<ImageDTO>  imageDTO { get; set; }
        public VehicleDTO vehicleDTO { get; set; }
        public List<ImageDTO> similarvehicleDTO { get; set; }
        public ImageDTO singleImageDTO { get; set; }
    }
}
