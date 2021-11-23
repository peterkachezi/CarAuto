using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMatt.Data.DTOs.ApplicationUserServiceModule
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ResetLink { get; set; }
        public string FullName { get; set; }
    }
}

