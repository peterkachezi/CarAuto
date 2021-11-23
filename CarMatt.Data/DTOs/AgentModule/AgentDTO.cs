using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.AgentModule
{
    public class AgentDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
