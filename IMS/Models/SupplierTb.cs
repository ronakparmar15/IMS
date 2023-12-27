using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class SupplierTb
    {
        public SupplierTb()
        {
            PurchaseTb = new HashSet<PurchaseTb>();
        }
        [Display(Name = "Supplier Id")]

        public int SupId { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupName { get; set; }
        [Display(Name = "Supplier Address")]
        public string SupAddress { get; set; }
        [Display(Name = "Supplier State")]
        public string SupType { get; set; }
        [Display(Name = "Supplier Mobile")]
        public decimal SupMobile { get; set; }
        [Display(Name = "Supplier GST No")]
        public string SupGstNumber { get; set; }

        public virtual ICollection<PurchaseTb> PurchaseTb { get; set; }
    }
}
