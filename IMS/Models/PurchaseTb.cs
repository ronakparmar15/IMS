using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class PurchaseTb
    {
        public int PurId { get; set; }
        public DateTime? PurDate { get; set; }
        public int? SupId { get; set; }
        public int? PurByUid { get; set; }
        public double? TotalAmount { get; set; }
        public double? CgstAmount { get; set; }
        public double? SgstAmount { get; set; }
        public double? IgstAmount { get; set; }
        public double? GrandTotal { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
