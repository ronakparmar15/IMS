using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class stokes
    {
        [Key]

        [Display(Name = "Stokes Id")]
        public int Sno { get; set; }
        [Display(Name = "Item Id")]
        public int ItemId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Purchase Quentity")]
        public int PurchaseQty { get; set; }
        [Display(Name = "Sales Quentity")]
        public int SalesQty { get; set; }
        [Display(Name = "Net Quentity")]
        public int NetQty { get; set; }
        [Display(Name = "Purchase Total3")]
        public decimal PurchaseTotal3 { get; set; }
        [Display(Name = "Sales Total3")]
        public decimal SalesTotal3 { get; set; }
        [Display(Name = "Net Total")]
        public decimal NetTotal { get; set; }

        //public IEnumerable<PurchaseTb> PurchaseData { get; set; }
        //public IEnumerable<SalesTb> SalesData { get; set; }

    }
}
