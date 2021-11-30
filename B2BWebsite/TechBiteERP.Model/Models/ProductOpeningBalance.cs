using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class ProductOpeningBalance
    {
        [Key]
        public int Id { get; set; }
         
        public string ItemId { get; set; }
        public  string ItemCode { get; set; }
        public  string ItemName { get; set; }

        public decimal OpeningBalance { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
