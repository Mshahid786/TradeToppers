using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
  public  class PaymentHistory
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Block { get; set; }
        public int PlotId { get; set; }
        public int PayType { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCNIC { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal DealAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public int PaymentType { get; set; }
        public string DepositeSlip { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public bool Deleted { get; set; }

        public Item Plot { get; set; }
     

    }
}
