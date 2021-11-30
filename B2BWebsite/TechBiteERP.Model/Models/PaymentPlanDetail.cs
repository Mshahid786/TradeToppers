using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class PaymentPlanDetail
    {
        [Key]
        public int PlanDetailId { get; set; }
        public int PaymentPlanId { get; set; }
        public int BubbleDuration { get; set; }
        public int Sr { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
    }
}
