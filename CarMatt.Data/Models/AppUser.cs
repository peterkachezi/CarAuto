using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool isActive { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
