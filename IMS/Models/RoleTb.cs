using System;
using System.Collections.Generic;

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

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public byte RoleStatus { get; set; }
        public byte PermissionCreateUpdate { get; set; }
        public byte PermissionDelete { get; set; }
        public byte PermissionView { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<UserTb> UserTb { get; set; }
    }
}
