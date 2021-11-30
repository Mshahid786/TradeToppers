using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class City
    {
        public int CityID { get; set; }
        public DateTime CreationDate { get; set; }
        public string CityName { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        public int CountryId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}
