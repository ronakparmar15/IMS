using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class SalesDetailTb
    {
        [Display(Name = "Sale Detaile Id")]

        public int SalesDetailId { get; set; }
        [Display(Name = "Sales Id")]
        public int? SalesId { get; set; }
        [Display(Name = "Item Id")]
        public int? ItemId { get; set; }
        [Display(Name = "Qty")]
        public int? Qty { get; set; }
        [Display(Name = "Unit Price")]
        public double? UnitPrice { get; set; }
        [Display(Name = "Total Price")]
        public double? TotalPrice { get; set; }
        [Display(Name = "GSTID")]
        public int? GstId { get; set; }
    }
}
