using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
    public class ApplicationRole
    {
        public int Id { get; set; }

        [Required]
       // public virtual AppMenu Menu { get; set; }
        public int MenuId { get; set; }
        [Required]
        public string RoleId { get; set; }
        public virtual IdentityRole Role { get; set; }

        //public bool isRead { get; set; }
        //public bool isAdd { get; set; }
        //public bool FullControl { get; set; }

    }
}
