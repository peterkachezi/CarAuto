using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.ApplicationUserServiceModule
{
    public class ApplicationUserDTO
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool isActive { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Discriminator { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }


    }
}
