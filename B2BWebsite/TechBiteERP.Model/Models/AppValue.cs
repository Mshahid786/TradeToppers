using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class AppValue
    {
        public int Id { get; set; }
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        public ApplicationCompany Company { get; set; }

        public int BaseId { get; set; }
        public AppValueBase Base { get; set; }

        [MaxLength(50)]
        [Display(Name = "Module Name")]
        public string Module { get; set; }
        [MaxLength(50)]
        [Display(Name = "Config Name")]
        public string ConfigName { get; set; }
        [MaxLength(200)]
        [Display(Name = "Config Value")]
        public string ConfigValue { get; set; }
        [MaxLength(250)]
        [Display(Name = "Config Description")]
        public string ConfigDescription { get; set; }
        [MaxLength(50)]
        [Display(Name = "Value1")]
        public string Value1 { get; set; }
        [MaxLength(50)]
        [Display(Name = "Value2")]
        public string Value2 { get; set; }
        [MaxLength(50)]
        [Display(Name = "Value3")]
        public string UserValue3 { get; set; }
        [MaxLength(50)]
        [Display(Name = "Value4")]
        public string UserValue4 { get; set; }
        [Display(Name = "Created By")]
        [MaxLength(450)]
        public string CreatedBy { get; set; }
        [Display(Name = "Updated By")]
        [MaxLength(450)]
        public string UpdatedBy { get; set; }


        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
