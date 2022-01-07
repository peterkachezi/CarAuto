using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarMatt.Data.Models
{
    public class PartImage
    {
        public Guid Id { get; set; }
        public Guid PartId { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }


    }
}
