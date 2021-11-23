using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace CarMatt.Data.Models
{
    public partial class County
    {
        //[Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }


    }
}
