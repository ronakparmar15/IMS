using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class ClassTb
    {
        public ClassTb()
        {
            ItemTb = new HashSet<ItemTb>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public byte ClassStatus { get; set; }
        public int? GstId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ItemTb> ItemTb { get; set; }
    }
}
