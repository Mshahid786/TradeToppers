using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class OpeningBalance
    {
        public int Id { get; set; }
        public int FinancialYearID { get; set; }
        public int AccountId { get; set; }
        public int CostCenterId { get; set; }
        public int WareHouseId { get; set; }
        public decimal Balance { get; set; }
        public bool Dr { get; set; }
        public bool Cr { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
