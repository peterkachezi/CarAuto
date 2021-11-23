﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMatt.Data.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Images = new HashSet<Image>();
        }

        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public string Make { get; set; }

        public long AvailabilityStatus { get; set; }

        public string Model { get; set; }

        public string Kilometres { get; set; }

        public string BodyType { get; set; }

        public string StyleTrim { get; set; }

        public string Engine { get; set; }

        public string Drivetrain { get; set; }

        public string Transmission { get; set; }

        public string ExteriorColor { get; set; }

        public string InteriorColor { get; set; }

        public string Passangers { get; set; }

        public string FuelType { get; set; }

        public string CityFuelEconomy { get; set; }

        public string HighWayFuelEconomy { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}