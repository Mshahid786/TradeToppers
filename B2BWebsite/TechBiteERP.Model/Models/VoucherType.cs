using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class VoucherType
    {
        [Key]
        public int VoucherTypesID { get; set; }
        public string Type { get; set; }
        public string VoucherTypeDescription { get; set; }
        public string VoucherTypeRemarks { get; set; }
        public bool IsAutoPost { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
