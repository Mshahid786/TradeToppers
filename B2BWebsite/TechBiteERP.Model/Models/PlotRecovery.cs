using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class PlotRecovery
    {
        [Key]
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Client { get; set; }
        public int LoginId { get; set; }
        public string Block { get; set; }
        public int PlotId { get; set; }
        public string Stations { get; set; }
        public string FormNo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public int CompanyId { get; set; }
        public int FinancialYearId { get; set; }
        public int BranchId { get; set; }
        public bool Deleted { get; set; }

    }
}
