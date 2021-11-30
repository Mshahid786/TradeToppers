using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class PlotCancellation
    {
        [Key]
        public int Id { get; set; }
        public string Block { get; set; }
        public int PlotId { get; set; }
        public int ActionType { get; set; }
        [DataType(DataType.Date)]
        public DateTime LogDate { get; set; }
        public string CourierRef { get; set; }
        [DataType(DataType.Date)]
        public DateTime CourierDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public bool Deleted { get; set; }
        public int FinancialYearId { get; set; }
        public DateTime UpdationDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
    }
}
