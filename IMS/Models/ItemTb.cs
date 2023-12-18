using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class ItemTb
    {
        public ItemTb()
        {
            PurchaseTb = new HashSet<PurchaseTb>();
            SalesTb = new HashSet<SalesTb>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemPurchaseRate { get; set; }
        public int ItemSalesRate { get; set; }
        public byte ItemStatus { get; set; }
        public int ItemClassId { get; set; }
        public string ItemHsn { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ClassTb ItemClass { get; set; }
        public virtual ICollection<PurchaseTb> PurchaseTb { get; set; }
        public virtual ICollection<SalesTb> SalesTb { get; set; }
    }
}
