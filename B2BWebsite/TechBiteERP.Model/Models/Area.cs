using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class Area
    {
        public int AreaID { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public string AreaName { get; set; }
        public int CountryId { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
    }
}
