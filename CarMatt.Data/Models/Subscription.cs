using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMatt.Data.Models
{
    public class Subscription
    {
  
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
