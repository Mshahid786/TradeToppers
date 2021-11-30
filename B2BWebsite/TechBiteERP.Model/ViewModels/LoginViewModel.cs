using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.ViewModels
{
  public  class LoginViewModel
    {
       
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public int CompanyId { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
