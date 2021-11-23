using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarMatt.Data.Models
{
   public partial class Agent 
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }



    }
}
