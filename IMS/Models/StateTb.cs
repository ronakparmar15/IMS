using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class StateTb
    {
        [Display(Name = "State Id")]

        public int StateId { get; set; }
        [Display(Name = "State Name")]

        public string StateName { get; set; }
    }
}
