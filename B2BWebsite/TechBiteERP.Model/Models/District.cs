using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class District
    {
        public int DistrictID { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public string DistrictName { get; set; }
        public int CountryId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
