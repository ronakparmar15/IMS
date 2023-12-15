using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class SupplierTb
    {
        public int SupId { get; set; }
        public string SupName { get; set; }
        public string SupAddress { get; set; }
        public string SupType { get; set; }
        public decimal SupMobile { get; set; }
        public string SupGstNumber { get; set; }
    }
}
