using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class PriceListNewDetail
    {
        [Key]
        public int PriceDetailId { get; set; }
        public int PriceListNewId { get; set; }
        public int Sr { get; set; }
        public decimal MRPrice { get; set; }
        public decimal TradePrice { get; set; }
        public decimal RP { get; set; }
        public decimal DiscountPer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comments { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
    }
}
