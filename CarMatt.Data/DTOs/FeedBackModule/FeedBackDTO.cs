using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.FeedBackModule
{
    public class FeedBackDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
                public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string Message { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
