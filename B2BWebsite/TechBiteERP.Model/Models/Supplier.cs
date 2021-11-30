using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
   public class Supplier
    {

        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Supplier GL Code")]
        public int SupplierAccountId { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        [Display(Name = "Reg. S.Tax No")]
        public string STN { get; set; }
        [Display(Name = "N.I.Tax No")]
        public string NTN { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "Fax No")]
        public string FaxNo { get; set; }
        public int To { get; set; }
        [Display(Name = "Fax No")]
        public int CreditLimitAccount { get; set; }
        public string CC { get; set; }
        [Display(Name = "Credit Days")]
        public int CreditDays { get; set; }
        public string BCC { get; set; }
        [Display(Name = "Comments")]
        public string Comments { get; set; }
        [Display(Name = "Check Cr Limit")]
        public bool checkCrLimit { get; set; }
        [Display(Name = "Black Listed")]
        public bool BlackListed { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        public bool Deleted { get; set; }

    }
}
