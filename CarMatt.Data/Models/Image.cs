using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarMatt.Data.Models
{
    public partial class Image
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
