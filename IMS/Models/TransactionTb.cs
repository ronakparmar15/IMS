using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class TransactionTb
    {
        public int TrnId { get; set; }
        public string TrnType { get; set; }
        public double? Amount { get; set; }
        public string TrnTrnType { get; set; }
        public byte? Remark { get; set; }
    }
}
