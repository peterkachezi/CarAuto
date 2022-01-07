using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.PartModule
{
    public class PartDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public List<string> ImageName { get; set; }
         public DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
    }
}
