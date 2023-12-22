using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class RoleTb
    {
        public RoleTb()
        {
            UserTb = new HashSet<UserTb>();
        }
        [Display(Name = "Role Id")]

        public int RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Role Status")]
        public byte RoleStatus { get; set; }
        [Display(Name = "Add & Update")]
        public byte PermissionCreateUpdate { get; set; }
        [Display(Name = "Delete")]
        public byte PermissionDelete { get; set; }
        [Display(Name = "View")]
        public byte PermissionView { get; set; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<UserTb> UserTb { get; set; }
        public string StatusDisplay
        {
            get
            {
                return RoleStatus == 1 ? "Active" : "Inactive";
            }
        }
        public string createupdate
        {
            get
            {
                return PermissionCreateUpdate == 1 ? "Yes" : "No";
            }
        }
        public string delete
        {
            get
            {
                return PermissionDelete == 1 ? "Yes" : "No";
            }
        }
        public string view
        {
            get
            {
                return PermissionView == 1 ? "Yes" : "No";
            }
        }
    }
}
