using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMatt.Data.Models
{
    public class CarModel
    {
        public Guid Id { get; set; }
        public Guid CarMakeId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }


    }
}
