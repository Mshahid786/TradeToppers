using System;
using System.Collections.Generic;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class Currency
    {
        public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public int Code { get; set; }
        public string CurrencyDescription { get; set; }
        public string Symbol { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
