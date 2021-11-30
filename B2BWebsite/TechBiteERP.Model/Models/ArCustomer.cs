using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class ArCustomer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        [Required]
        public string CNIC { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Address { get; set; }
        public int DiscountType { get; set; }
        public int DiscountPer { get; set; }
        public int AnnualDiscountType { get; set; }
        public int CustomerType { get; set; }
        public int AnnualDiscountPer { get; set; }
        public decimal InvoiceDiscount { get; set; }
        public decimal ChequeAmount { get; set; }
        public int CreditDays { get; set; }
        public decimal CreditLimitAmount { get; set; }
        public bool CheckCrLimit { get; set; }
        public bool Blocked { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "  GL Code")]
        public int GLCode { get; set; }
 
        [Display(Name = "Province")]
        public int SalesPersonId { get; set; }
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        public bool Deleted { get; set; }

        //Navigation Property


    }
}
