using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class GLSAccount
    {
        public int Id { get; set; }
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter Prefix")]
        public string Code { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CompanyId { get; set; }
        public ApplicationCompany Company { get; set; }
        [MaxLength(450)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(450)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
