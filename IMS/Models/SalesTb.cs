using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class SalesTb
    {
        [Display(Name = "Sales Id")]
        public int SalesId { get; set; }
        [Display(Name = "Sales Type")]
        public string SalesTyep { get; set; }
        [Display(Name = "Sales Date")]
        public DateTime SalesDate { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Customer Mobile")]
        public decimal CustomerMobile { get; set; }
        [Display(Name = "Customer State")]
        public string CustomerType { get; set; }
        [Display(Name = "Item Id")]
        public int ItemId { get; set; }
        [Display(Name = "Qty")]
        public int Qty { get; set; }
        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }
        [Display(Name = "Total-1")]
        public double Total1 { get; set; }
        [Display(Name = "Discount")]
        public double? Discount { get; set; }
        [Display(Name = "Total-2")]
        public double Total2 { get; set; }
        [Display(Name = "GSTID")]
        public int GstId { get; set; }
        [Display(Name = "CGST Amount")]
        public double CgstAmount { get; set; }
        [Display(Name = "SGST Amount")]
        public double SgstAmount { get; set; }
        [Display(Name = "IGST Amount")]
        public double IgstAmount { get; set; }
        [Display(Name = "Total-3")]
        public double Total3 { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        public virtual GstTb Gst { get; set; }
        public virtual ItemTb Item { get; set; }
        public virtual UserTb User { get; set; }
    }
}
