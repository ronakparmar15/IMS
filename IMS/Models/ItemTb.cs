using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Item Id")]

        public int ItemId { get; set; }
        [Display(Name = "Item Name")]

        public string ItemName { get; set; }
        [Display(Name = "Purchase Rate")]

        public int ItemPurchaseRate { get; set; }
        
        [Display(Name = "Sales Rate")]
        public int ItemSalesRate { get; set; }
        [Display(Name = "Status")]
        public byte ItemStatus { get; set; }
        [Display(Name = "Item class Id")]
        public int ItemClassId { get; set; }
        [Display(Name = "HSN")]
        public string ItemHsn { get; set; }
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        public virtual ClassTb ItemClass { get; set; }
        public virtual ICollection<PurchaseTb> PurchaseTb { get; set; }
        public virtual ICollection<SalesTb> SalesTb { get; set; }
    }
}
