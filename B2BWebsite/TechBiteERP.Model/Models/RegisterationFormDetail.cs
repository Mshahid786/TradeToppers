using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class RegisterationFormDetail
    {
        [Key]
        public int Id { get; set; }
        public int FormNo { get; set; }
        public string FormCode { get; set; }
        public int RegisterationFormId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
