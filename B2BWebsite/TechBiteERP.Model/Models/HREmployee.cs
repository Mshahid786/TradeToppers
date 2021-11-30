using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class HREmployee
    {
        [Key]
        [Display(Name = "Employee")]
        public int EmployeeID { get; set; }
        [Display(Name = "Date")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime DateOFBirth { get; set; }
        public string Address { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "Province")]
        public int ProvinceID { get; set; }
        [Display(Name = "City")]
        public int CityID { get; set; }
        [Display(Name = "Phone No 1")]
        public string CellNo1 { get; set; }
        [Display(Name = "Phone No 2")]
        public string CellNo2 { get; set; }
        public string Email { get; set; }
        public int CompanyID { get; set; }
        [Display(Name = "Branch")]
        public int BranchID { get; set; }
    }
}
