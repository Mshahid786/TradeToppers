using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class ApplicationRoleMenu
    {
        public int Id { get; set; }

        [Required]
        public virtual ApplicationMenu Menu { get; set; }
        public int MenuId { get; set; }
        [Required]
        public string RoleId { get; set; }

        public bool Active { get; set; }
        public virtual IdentityRole Role { get; set; }

    }
}
