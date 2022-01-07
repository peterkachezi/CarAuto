using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.PartModule
{
    public class PartImageDTO
    {

        public Guid Id { get; set; }
        public Guid PartId { get; set; }
        public string ImageName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string CreatedByName { get; set; }



    }
}
