using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class UserTb
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Phone { get; set; }
        public byte Status { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual RoleTb Role { get; set; }
    }
}
