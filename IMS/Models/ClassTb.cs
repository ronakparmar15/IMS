using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class ClassTb
    {
        public ClassTb()
        {
            ItemTb = new HashSet<ItemTb>();
        }
        [Display(Name ="Class Id")]
        public int ClassId { get; set; }
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }
        [Display(Name = "CGST")]

        public decimal Cgst { get; set; }
        [Display(Name = "SGST")]

        public decimal Sgst { get; set; }
        [Display(Name = "IGST")]

        public decimal Igst { get; set; }
        [Display(Name = "Status")]

        public byte ClassStatus { get; set; }
        [Display(Name = "Created At")]

        public DateTime CreatedAt { get; set; }
        [Display(Name = "GSTID")]

        public int? GstId { get; set; }
      

        public virtual GstTb Gst { get; set; }
        public virtual ICollection<ItemTb> ItemTb { get; set; }
    }
}
