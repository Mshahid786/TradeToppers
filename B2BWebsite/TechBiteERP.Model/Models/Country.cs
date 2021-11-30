using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class Country
    {
        public int CountryID { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public string CountryName { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
