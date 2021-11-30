using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class Region
    {
        public int RegionID { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public string RegionName { get; set; }
        public int CountryId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
