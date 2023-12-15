using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class SalesDetailTb
    {
        public int SalesDetailId { get; set; }
        public int? SalesId { get; set; }
        public int? ItemId { get; set; }
        public int? Qty { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
        public int? GstId { get; set; }
    }
}
