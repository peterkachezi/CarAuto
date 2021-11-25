using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.CarModelModule
{
    public class ModelDTO
    {
        public Guid Id { get; set; }
        public Guid CarMakeId { get; set; }
        public string CarMakeName { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }

    }
}
