using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class UserTb
    {
        public UserTb()
        {
            PurchaseTb = new HashSet<PurchaseTb>();
            SalesTb = new HashSet<SalesTb>();
        }
        [Display(Name = "User Id")]

        public int UserId { get; set; }
        [Display(Name = "UserName")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Phone")]
        public decimal Phone { get; set; }
        [Display(Name = "Status")]
        public byte Status { get; set; }
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        public virtual RoleTb Role { get; set; }
        public virtual ICollection<PurchaseTb> PurchaseTb { get; set; }
        public virtual ICollection<SalesTb> SalesTb { get; set; }

        public string StatusDisplay
        {
            get
            {
                return Status == 1 ? "Active" : "Inactive";
            }
        }
    }
}
