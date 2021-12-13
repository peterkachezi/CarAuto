using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.SubscriptionModule
{
    public class SubscriptionDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
