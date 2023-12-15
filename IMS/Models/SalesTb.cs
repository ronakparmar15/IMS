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
        public string SalesType { get; set; }
        public DateTime? SalesDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public decimal? CustomerMobile { get; set; }
        public double? TotalAmount { get; set; }
        public double? CgstAmount { get; set; }
        public double? SgstAmount { get; set; }
        public double? IgstAmount { get; set; }
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
