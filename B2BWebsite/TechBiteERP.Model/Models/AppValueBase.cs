using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class AppValueBase
    {
        public int Id { get; set; }
        [Display(Name = "Module")] [MaxLength(50)] public string Module { get; set; }
        [Display(Name = "Module")] public int ModuleId { get; set; }
        [Display(Name = "Module")] public string ModuleName { get; set; }
        [Display(Name = "Description")] [MaxLength(200)] public string ModuleDescription { get; set; }
        public string MaxSize { get; set; }
        public int CompanyId { get; set; }

        [Display(Name = "Active")] public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Created By")] [MaxLength(450)] public string CreatedBy { get; set; }
        [Display(Name = "Updated By")] [MaxLength(450)] public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
