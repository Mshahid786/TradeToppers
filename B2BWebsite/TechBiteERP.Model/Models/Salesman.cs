using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class Salesman
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "SM GL Code")]
        public int SMGLCode { get; set; }
        [Display(Name = "Imprest GL Code")]
        public int ImprestGlCode { get; set; }
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        public bool Deleted { get; set; }

        //Navigation Property
 
    }
}
