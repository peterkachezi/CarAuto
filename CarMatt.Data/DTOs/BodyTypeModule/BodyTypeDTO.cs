using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.BodyTypeModule
{
    public class BodyTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreateDate { get; set; }

        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
    }
}
