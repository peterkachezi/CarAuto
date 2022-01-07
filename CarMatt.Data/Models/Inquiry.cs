using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.Models
{
    public class Inquiry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string PhoneNumber { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
