using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TechBiteERP.Model.Models
{
    public class PlotRegisterationForm
    {
        public int Id { get; set; }
        public int BlockId { get; set; }
        public string Block { get; set; }
        public int PlotId { get; set; }
        public string Size { get; set; }
        public string Dimensions { get; set; }
        public string Factors { get; set; }
        public decimal PlotPrice { get; set; }
        public int FromCustomerId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string NomineeName { get; set; }
        public string NomineeFatherName { get; set; }
        public string NomineeCNIC { get; set; }
        public string NomineeEmail { get; set; }
        public string NomineePhoneNo { get; set; }
        public string Nomineeaddress { get; set; }
        public string FormNo { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal RegisterCharges  { get; set; }
        public int Installments  { get; set; }
        public decimal DealPrice { get; set; }
        public int DealValidity { get; set; }
        public int DiscountFactor { get; set; }
        public int SalesPerson { get; set; }
        public int MMD { get; set; }
        public string LoggedBy { get; set; }
        public decimal MMDPercentage { get; set; }
        public decimal MMDPercentageAmount { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal NetPayable { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdationDate { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime TransferDate { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public bool Canceled { get; set; }
        [Required] [Display(Name = "Photo")] [MaxLength(450)] public string Photo { get; set; }
        [Required] [Display(Name = "Photo")] [MaxLength(450)] public string Photo2 { get; set; }
        [Required] [Display(Name = "NomineePhoto")] [MaxLength(450)] public string NomineePhoto { get; set; }
        [Required] [Display(Name = "NomineePhoto")] [MaxLength(450)] public string NomineePhoto2 { get; set; }

    }
}
