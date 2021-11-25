using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<string> Photos { get; set; }
    }
}
