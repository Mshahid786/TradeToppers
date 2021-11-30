using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.ViewModels
{
   public class UserViewModel
    {
        [MaxLength(450)]
        public string Id { get; set; }
        //public int? AppUserID { get; set; }
        public string FullName { get; set; }
        public bool IsMaster { get; set; }
        public string Photo { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
    }
}
