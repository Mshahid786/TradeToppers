using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class Branch
    {
        public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public int POSCode { get; set; }
        public string POSName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public int RegionId { get; set; }
        public int BranchTypeId { get; set; }
        public string Abbreviation { get; set; }
        [Display(Name = "Photo")] [MaxLength(350)] public string Photo { get; set; }
        public int CompanyId { get; set; }
    }
}
