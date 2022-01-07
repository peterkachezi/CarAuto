using System;
using System.Collections.Generic;
using System.Text;

namespace CarMatt.Data.DTOs.InquiryModule
{
    public class InquiryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string PhoneNumber { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string StatusDescription { get; set; }

    }
}
