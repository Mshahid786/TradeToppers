using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class ApplicationUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(150)]
        public string FullName { get; set; }
        [MaxLength(300)]
        public string Photo { get; set; }
        public bool IsMaster { get; set; }
        
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserType { get; set; }

    }
    public class AspNetUserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        
    }
}
