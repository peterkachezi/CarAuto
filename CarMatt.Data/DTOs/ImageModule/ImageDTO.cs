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
        public decimal Price { get; set; }
        public Guid MakeId { get; set; }
        public string MakeName { get; set; }
        public string FullName => MakeName + " " + ModelName;
        public long AvailabilityStatus { get; set; }
        public Guid ModelId { get; set; }
        public string ModelName { get; set; }
        public string Kilometres { get; set; }
        public Guid BodyTypeId { get; set; }
        public string BodyTypeName { get; set; }
        public string StyleTrim { get; set; }
        public string Engine { get; set; }
        public string Drivetrain { get; set; }
        public string Transmission { get; set; }
        public string ExteriorColor { get; set; }
        public string InteriorColor { get; set; }
        public string Doors { get; set; }
        public string FuelType { get; set; }
        public string CityFuelEconomy { get; set; }
        public string HighWayFuelEconomy { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedByName { get; set; }
        public int Quantity { get; set; }
        public string YearOfProduction { get; set; }
    }
}
