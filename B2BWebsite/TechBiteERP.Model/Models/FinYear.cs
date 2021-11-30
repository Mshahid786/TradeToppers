using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class FinYear
    {
        [Key]
        public int Id { get; set; }
        public string FYear { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public int YClose { get; set; }
        public int EXRate { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
    }
}
