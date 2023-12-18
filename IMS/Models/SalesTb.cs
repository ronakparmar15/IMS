using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class SalesTb
    {
        public int SalesId { get; set; }
        public string SalesTyep { get; set; }
        public DateTime SalesDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public decimal CustomerMobile { get; set; }
        public int ItemId { get; set; }
        public int Qty { get; set; }
        public double Total1 { get; set; }
        public double? Discount { get; set; }
        public double Total2 { get; set; }
        public int GstId { get; set; }
        public double CgstAmount { get; set; }
        public double SgstAmount { get; set; }
        public double IgstAmount { get; set; }
        public double Total3 { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual GstTb Gst { get; set; }
        public virtual ItemTb Item { get; set; }
        public virtual UserTb User { get; set; }
    }
}
