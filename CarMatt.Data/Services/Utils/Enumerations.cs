using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarMatt.Data.Services.Utils
{
    public class Enumerations
    {
        public enum AssetStatus
        {
            [Description("Good")]
            Good = 0,
            [Description("Bad")]
            Bad = 1,

        }

        public enum InquiryStatus
        {
            [Description("Pending")]
            Pending = 0,
            [Description("Seen")]
            Paid = 1,

        }


        public enum AccountStatus
        {
            [Description("Disabled")]
            Disabled = 0,
            [Description("Active")]
            Active = 1,

        }


    }
}
