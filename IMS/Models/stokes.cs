using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class stokes
    {
        [Key]
        public int Sno { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int PurchaseQty { get; set; }
        public int SalesQty { get; set; }
        public int NetQty { get; set; }
        public decimal PurchaseTotal3 { get; set; }
        public decimal SalesTotal3 { get; set; }
        public decimal NetTotal { get; set; }

        //public IEnumerable<PurchaseTb> PurchaseData { get; set; }
        //public IEnumerable<SalesTb> SalesData { get; set; }

    }
}
