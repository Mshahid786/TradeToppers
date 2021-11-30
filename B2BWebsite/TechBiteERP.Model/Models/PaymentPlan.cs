using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class PaymentPlan
    {
        [Key]
        public int Id { get; set; }
        public string BlockId { get; set; }
        public int PlotId { get; set; }
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        public decimal PlotPrice { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal Booking { get; set; }
        public decimal Confirmation { get; set; }
        public decimal Allotment { get; set; }
        public decimal Possesion { get; set; }
        public decimal Balance { get; set; }
        public int NoOfInstallment { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal BubbleAmount { get; set; } 
        public decimal QuartleyInstall { get; set; }
        public bool IsQuartleyInstall { get; set; }
      
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public bool Deleted { get; set; }
        public bool IsDefault { get; set; }
        public int FinancialYearId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public List<PaymentPlanDetail> PaymentPlanDetails { get; set; }

    }
}
