using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class RegisterationForms
    {
        [Key]
        public int Id { get; set; }
        public string FormCode { get; set; }
        public int FormNoStart { get; set; }
        public int FormNoEnd { get; set; }
        public bool Status { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public bool Deleted { get; set; }

        public Branch Branch { get; set; }
     

    }
}
