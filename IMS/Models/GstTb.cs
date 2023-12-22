using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "GSTID")]
        public int GstId { get; set; }
        [Display(Name = "GST Name")]
        public string GstName { get; set; }
        [Display(Name = "CGST")]
        public decimal Cgst { get; set; }
        [Display(Name = "SGST")]
        public decimal Sgst { get; set; }
        [Display(Name = "IGST")]
        public decimal Igst { get; set; }
        [Display(Name = "Active")]
        public byte Active { get; set; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ClassTb> ClassTb { get; set; }
        public virtual ICollection<PurchaseTb> PurchaseTb { get; set; }
        public virtual ICollection<SalesTb> SalesTb { get; set; }
        public string StatusDisplay
        {
            get
            {
                return Active == 1 ? "Active" : "Inactive";
            }
        }
    }
}
