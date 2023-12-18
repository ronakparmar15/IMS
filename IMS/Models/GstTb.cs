using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class GstTb
    {
        public GstTb()
        {
            ClassTb = new HashSet<ClassTb>();
            PurchaseTb = new HashSet<PurchaseTb>();
            SalesTb = new HashSet<SalesTb>();
        }

        public int GstId { get; set; }
        public string GstName { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public byte Active { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ClassTb> ClassTb { get; set; }
        public virtual ICollection<PurchaseTb> PurchaseTb { get; set; }
        public virtual ICollection<SalesTb> SalesTb { get; set; }
    }
}
